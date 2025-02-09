using IctuTaekwondo.Api.Data;
using IctuTaekwondo.Api.Mappers;
using IctuTaekwondo.Api.Models;
using IctuTaekwondo.Shared.Responses.Achievement;
using IctuTaekwondo.Shared.Responses;
using IctuTaekwondo.Shared.Responses.Event;
using IctuTaekwondo.Shared.Schemas.Event;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using IctuTaekwondo.Shared.Enums;

namespace IctuTaekwondo.Api.Services
{
    public interface IEventService
    {
        Task<EventResponse?> CreateAsync(EventCreateSchema schema);
        Task<EventResponse?> UpdateAsync(int id, EventUpdateSchema schema);
        Task<bool> DeleteAsync(int id);
        Task<PaginationResponse<EventResponse>> GetAllAsync(int page, int size);
        Task<EventResponse?> FindByIdAsync(int id, string? userId);
        Task<PaginationResponse<EventResponse>> FilterAsync(
            int page,
            int size,
            string? name = null,
            EventStatus? status = null);
        List<EventStatus> GetStatus(Event @event, string? userId);
    }

    public class EventService : IEventService
    {
        private readonly ILogger<EventService> _logger;
        private readonly ApiDbContext _context;

        public EventService(ILogger<EventService> logger, ApiDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<EventResponse?> CreateAsync(EventCreateSchema schema)
        {
            var newEvent = new Event
            {
                Name = schema.Name,
                StartDate = schema.StartDate,
                EndDate = schema.EndDate,
                Location = schema.Location,
                Fee = schema.Fee,
                MaxParticipants = schema.MaxParticipants,
                Description = schema.Description,
                CreatedAt = DateTime.UtcNow,
            };

            _context.Events.Add(newEvent);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                var response = await FindByIdAsync(newEvent.Id, null);
                return newEvent.ToEventResponse();
            }
            return null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var @event = _context.Events.FirstOrDefault(e => e.Id == id);
            if (@event == null)
            {
                _logger.LogError("Event not found: {0}", id);
                throw new Exception("Sự kiện không tồn tại");
            }

            _context.Events.Remove(@event);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<PaginationResponse<EventResponse>> GetAllAsync(int page, int size)
        {
            var events = await _context.Events
                .Include(e => e.EventRegistrations)
                .ThenInclude(er => er.User)
                .ToListAsync();

            var response = events
                .Skip((page - 1) * size)
                .Take(size)
                .Select(e =>
                {
                    var r = e.ToEventResponse();
                    r.Status = GetStatus(e, null);
                    return r;
                })
                .ToList();

            return new PaginationResponse<EventResponse>(page, size, events.Count, response);
        }

        public async Task<PaginationResponse<EventResponse>> FilterAsync(
            int page,
            int size,
            string? name = null,
            EventStatus? status = null)
        {
            var query = _context.Events.AsQueryable();

            if (!string.IsNullOrEmpty(name)) query = query.Where(p => EF.Functions.Like(p.Name, $"%{name}%"));

            if (status.HasValue)
            {
                if (status.Value == EventStatus.NotStarted)
                    query = query.Where(p => p.StartDate > DateTime.UtcNow);

                else if (status.Value == EventStatus.Started)
                    query = query.Where(p => p.StartDate <= DateTime.UtcNow && p.EndDate > DateTime.UtcNow);

                else if (status.Value == EventStatus.Ended)
                    query = query.Where(p => p.EndDate.HasValue && p.EndDate.Value <= DateTime.UtcNow);
            }

            var events = await query
                .Include(e => e.EventRegistrations)
                .ThenInclude(er => er.User)
                .ToListAsync();

            var response = events
                .Skip((page - 1) * size)
                .Take(size)
                .Select(e =>
                {
                    var r = e.ToEventResponse();
                    r.Status = GetStatus(e, null);
                    return r;
                })
                .ToList();

            return new PaginationResponse<EventResponse>(page, size, events.Count, response);
        }

        public async Task<EventResponse?> FindByIdAsync(int id, string? userId)
        {
            var @event = await _context.Events
                .Include(e => e.EventRegistrations)
                .ThenInclude(er => er.User)
                .FirstOrDefaultAsync(e => e.Id == id);
            if (@event == null)
            {
                _logger.LogError("Event not found: {0}", id);
                return null;
            }

            var response = @event.ToEventResponse();
            response.Status = GetStatus(@event, userId);
            return response;
        }

        public async Task<EventResponse?> UpdateAsync(int id, EventUpdateSchema schema)
        {
            var @event = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);

            if (@event == null)
            {
                _logger.LogError("Event not found: {0}", id);
                throw new Exception("Sự kiện không tồn tại");
            }

            @event.Name = schema.Name;
            @event.StartDate = schema.StartDate;
            @event.EndDate = schema.EndDate;
            @event.Location = schema.Location;
            @event.Fee = schema.Fee;
            @event.MaxParticipants = schema.MaxParticipants;
            @event.Description = schema.Description;

            _context.Entry(@event).State = EntityState.Modified;
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return @event.ToEventResponse();
            }
            return null;
        }

        public List<EventStatus> GetStatus(Event @event, string? userId)
        {
            var status = new List<EventStatus>();

            if (@event.EndDate.HasValue && @event.EndDate.Value < DateTime.UtcNow)
            {
                status.Add(EventStatus.Ended);
            }
            else if (@event.StartDate < DateTime.UtcNow)
            {
                status.Add(EventStatus.Started);
            }
            else
            {
                status.Add(EventStatus.NotStarted);
            }

            if (@event.MaxParticipants.HasValue && @event.EventRegistrations.Count >= @event.MaxParticipants.Value)
            {
                status.Add(EventStatus.Full);
            }

            if (!string.IsNullOrEmpty(userId) && @event.EventRegistrations.Any(er => er.UserId == userId))
            {
                status.Add(EventStatus.Registered);
            }

            return status;
        }
    }
}
