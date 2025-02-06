using System.Net;
using IctuTaekwondo.Api.Data;
using IctuTaekwondo.Api.Mappers;
using IctuTaekwondo.Api.Models;
using IctuTaekwondo.Shared.Responses;
using IctuTaekwondo.Shared.Responses.Event;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IctuTaekwondo.Api.Services
{
    // Enum đại diện cho các kết quả có thể có của một thao tác đăng ký sự kiện  
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

    // Các phương thức mở rộng cho enum EventRegisterationResult  
    public static class EventRegisterationExtension
    {
        // Phương thức để lấy văn bản trạng thái dựa trên EventRegisterationResult  
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
                _ => "Lỗi không xác định."
            };
        }
    }

    public interface IEventRegisterationService
    {
        Task<EventRegisterationResult> RegisterAsync(int id, string? userId, bool isCheckValid = false);
        Task<EventRegisterationResult> UnregisterAsync(int id, string? userId);
        Task<Event?> GetAllAsync(int id);
        public PaginationResponse<UserResgiteredEventResponse> GetAllRegisteration(Event @event, int page, int size);
    }

    // Lớp dịch vụ để xử lý các thao tác đăng ký sự kiện  
    public class EventRegisterationService : IEventRegisterationService
    {
        private readonly ApiDbContext _context;

        // Constructor để khởi tạo ngữ cảnh cơ sở dữ liệu  
        public EventRegisterationService(ApiDbContext context)
        {
            _context = context;
        }

        // Phương thức để đăng ký người dùng cho một sự kiện  
        public async Task<EventRegisterationResult> RegisterAsync(int id, string? userId, bool isCheckValid = false)
        {
            // Lấy sự kiện cùng với các đăng ký của nó  
            var @event = await GetAllAsync(id);
            if (@event == null) return EventRegisterationResult.EventNotFound;

            // Kiểm tra xem sự kiện có hợp lệ để đăng ký không  
            if (isCheckValid)
            {
                var errorMessage = IsEventValidToRegister(@event);
                if (errorMessage != EventRegisterationResult.Success) return errorMessage;
            }

            // Kiểm tra xem người dùng đã đăng ký sự kiện chưa  
            var registration = @event.EventRegistrations.FirstOrDefault(er => er.UserId == userId);
            if (registration != null) return EventRegisterationResult.AlreadyRegistered;

            // Tạo một đăng ký mới  
            var newRegistration = new EventRegistration
            {
                EventId = id,
                UserId = userId!,
                CreatedAt = DateTime.UtcNow,
            };

            // Thêm đăng ký mới vào ngữ cảnh  
            _context.EventRegistrations.Add(newRegistration);

            // Lưu các thay đổi vào cơ sở dữ liệu  
            var result = await _context.SaveChangesAsync();
            if (result == 0) return EventRegisterationResult.Error;

            return EventRegisterationResult.Success;
        }

        // Phương thức để hủy đăng ký người dùng khỏi một sự kiện  
        public async Task<EventRegisterationResult> UnregisterAsync(int id, string? userId)
        {
            // Lấy sự kiện cùng với các đăng ký của nó  
            var @event = await GetAllAsync(id);
            if (@event == null) return EventRegisterationResult.EventNotFound;

            // Tìm đăng ký của người dùng  
            var registration = @event.EventRegistrations.FirstOrDefault(er => er.UserId == userId);
            if (registration == null) return EventRegisterationResult.NotRegistered;

            // Xóa đăng ký khỏi ngữ cảnh  
            _context.EventRegistrations.Remove(registration);

            // Lưu các thay đổi vào cơ sở dữ liệu  
            var result = await _context.SaveChangesAsync();
            if (result == 0) return EventRegisterationResult.Error;

            return EventRegisterationResult.Success;
        }

        // Phương thức để lấy một sự kiện cùng với các đăng ký của nó theo ID sự kiện  
        public async Task<Event?> GetAllAsync(int id)
        {
            return await _context.Events
                .Include(e => e.EventRegistrations)
                .ThenInclude(er => er.User)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        // Phương thức để kiểm tra xem sự kiện có hợp lệ để đăng ký không  
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

        // Phương thức lấy tất cả người dùng đã đăng ký của sự kiện
        public PaginationResponse<UserResgiteredEventResponse> GetAllRegisteration(Event @event, int page, int size)
        {
            var registrations = @event.EventRegistrations
                .Skip((page - 1) * size)
                .Take(size)
                .Select(er =>
                {
                    var user = er.User.ToUserResgiteredEventResponse();
                    user.RegisteredAt = er.CreatedAt;
                    return user;
                })
                .ToList();

            return new PaginationResponse<UserResgiteredEventResponse>(
                page, size,
                registrations.Count,
                registrations);
        }
    }
}
