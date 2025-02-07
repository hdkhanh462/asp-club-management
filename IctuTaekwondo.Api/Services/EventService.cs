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
        Task<EventFullDetailResponse?> CreateAsync(EventCreateSchema schema);
        Task<bool> UpdateAsync(int id, EventUpdateSchema schema);
        Task<bool> DeleteAsync(int id);
        Task<PaginationResponse<EventResponse>> GetAllAsync(int page, int size);
        Task<EventFullDetailResponse?> GetByIdAsync(int id);
        Task<PaginationResponse<EventResponse>> GetAllWithFilterAsync(
            int page,
            int size,
            string? name = null,
            EventStatus? status = null);
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

        public async Task<EventFullDetailResponse?> CreateAsync(EventCreateSchema schema)
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
                var response = await GetByIdAsync(newEvent.Id);
                return newEvent.ToEventFullDetailResponse();
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
            var events = await _context.Events.ToListAsync();

            return new PaginationResponse<EventResponse>(
                page, size,
                events.Count,
                events.Skip((page - 1) * size).Take(size)
                .Select(e => e.ToEventResponse()).ToList());
        }

        public async Task<PaginationResponse<EventResponse>> GetAllWithFilterAsync(
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

            var events = await query.ToListAsync();

            return new PaginationResponse<EventResponse>(
                page, size,
                events.Count,
                events.Skip((page - 1) * size).Take(size)
                .Select(e => e.ToEventResponse()).ToList());
        }

        public async Task<EventFullDetailResponse?> GetByIdAsync(int id)
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

            return @event.ToEventFullDetailResponse();
        }

        public async Task<bool> UpdateAsync(int id, EventUpdateSchema schema)
        {
            var @event = await _context.Events
                .Include(e => e.EventRegistrations)
                .ThenInclude(er => er.User)
                .FirstOrDefaultAsync(e => e.Id == id);

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

            return result > 0;
        }
    }
}
