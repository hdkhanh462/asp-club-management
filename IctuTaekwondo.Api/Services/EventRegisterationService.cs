using IctuTaekwondo.Api.Data;
using IctuTaekwondo.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace IctuTaekwondo.Api.Services
{
    public enum EventRegisterationResult
    {
        Success,
        Error,
        EventNotFound,
        AlreadyRegistered,
        NotRegistered,
        EventEnded,
        EventStarted,
        EventFull
    }

    public static class EventRegisterationExtension
    {
        public static string GetStatusText(this EventRegisterationResult result, string alias = "Người dùng")
        {
            return result switch
            {
                EventRegisterationResult.Success => "Thành công.",
                EventRegisterationResult.Error => "Lỗi cơ sở dữ liệu.",
                EventRegisterationResult.EventNotFound => "Không tìm thấy sự kiện.",
                EventRegisterationResult.AlreadyRegistered => $"{alias} đã đăng ký sự kiện này rồi.",
                EventRegisterationResult.NotRegistered => $"{alias} chưa đăng ký sự kiện này.",
                EventRegisterationResult.EventEnded => "Sự kiện đã kết thúc.",
                EventRegisterationResult.EventStarted => "Sự kiện đã bắt đầu.",
                EventRegisterationResult.EventFull => "Sự kiện đã đạt giới hạn số lượng người tham gia.",
                _ => "Lõi không xác định."
            };
        }
    }

    public class EventRegisterationService
    {
        private readonly ApiDbContext _context;
        public EventRegisterationService(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<EventRegisterationResult> RegisterEvent(int id, string? userId, bool isCheckValid = false)
        {
            var @event = await GetEventWithRegisterationAsync(id);
            if (@event == null) return EventRegisterationResult.EventNotFound;

            if (isCheckValid)
            {
                var errorMessage = IsEventValidToRegister(@event);
                if (errorMessage != EventRegisterationResult.Success) return errorMessage;
            }

            var registration = @event.EventRegistrations.FirstOrDefault(er => er.UserId == userId);
            if (registration != null) return EventRegisterationResult.AlreadyRegistered;

            var newRegistration = new EventRegistration
            {
                EventId = id,
                UserId = userId,
            };

            _context.EventRegistrations.Add(newRegistration);

            var result = await _context.SaveChangesAsync();
            if (result == 0) return EventRegisterationResult.Error;

            return EventRegisterationResult.Success;
        }
        public async Task<EventRegisterationResult> UnregisterEvent(int id, string? userId)
        {
            var @event = await GetEventWithRegisterationAsync(id);
            if (@event == null) return EventRegisterationResult.EventNotFound;

            var registration = @event.EventRegistrations.FirstOrDefault(er => er.UserId == userId);
            if (registration == null) return EventRegisterationResult.NotRegistered;

            _context.EventRegistrations.Remove(registration);

            var result = await _context.SaveChangesAsync();
            if (result == 0) return EventRegisterationResult.Error;

            return EventRegisterationResult.Success;
        }

        public async Task<Event?> GetEventWithRegisterationAsync(int id)
        {
            return await _context.Events
                .Include(e => e.EventRegistrations)
                .ThenInclude(er => er.User)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        private EventRegisterationResult IsEventValidToRegister(Event @event)
        {
            if (@event.EndDate.HasValue && @event.EndDate.Value < DateTime.Now)
            {
                return EventRegisterationResult.EventEnded;
            }

            if (@event.StartDate < DateTime.Now)
            {
                return EventRegisterationResult.EventStarted;
            }

            if (@event.MaxParticipants.HasValue && @event.EventRegistrations.Count >= @event.MaxParticipants.Value)
            {
                return EventRegisterationResult.EventFull;
            }

            return EventRegisterationResult.Success;
        }
    }
}
