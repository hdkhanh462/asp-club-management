using System.Buffers;
using IctuTaekwondo.Api.Data;
using IctuTaekwondo.Api.Mappers;
using IctuTaekwondo.Api.Models;
using IctuTaekwondo.Shared.Enums;
using IctuTaekwondo.Shared.Responses;
using IctuTaekwondo.Shared.Responses.Achievement;
using IctuTaekwondo.Shared.Responses.Finance;
using IctuTaekwondo.Shared.Schemas.Achievement;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IctuTaekwondo.Api.Services
{
    public interface IAchievementService
    {
        Task<AchievementResponse?> CreateAsync(AchievementCreateSchema schema);
        Task<AchievementResponse?> UpdateAsync(int id, AchievementUpdateSchema schema);
        Task<bool> DeleteAsync(int id);
        Task<Paginator<AchievementResponse>> GetAllAsync(int page, int size);
        Task<AchievementResponse?> FindByIdAsync(int id);
        Task<Paginator<AchievementResponse>> GetAllWithFilterAsync(
            int page,
            int size,
            string? name = null,
            string? userId = null,
            int? eventId = null,
            DateOnly? startDate = null,
            DateOnly? endDate = null);
    }

    public class AchievementService : IAchievementService
    {
        private readonly ILogger<AchievementService> _logger;
        private readonly ApiDbContext _context;
        private readonly UserManager<User> _userManager;

        public AchievementService(ILogger<AchievementService> logger, ApiDbContext context, UserManager<User> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public async Task<AchievementResponse?> CreateAsync(AchievementCreateSchema schema)
        {
            var isValid = await Validate(schema.UserId, schema.EventId);
            if (!string.IsNullOrEmpty(isValid)) throw new ArgumentException(isValid);

            var newAchievement = new Achievement
            {
                Name = schema.Name,
                DateAchieved = schema.DateAchieved,
                Description = schema.Description,
                CreatedAt = DateTime.UtcNow,
                UserId = schema.UserId,
                EventId = schema.EventId,
            };

            _context.Achievements.Add(newAchievement);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
                return newAchievement.ToAchievementResponse();

            return null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var achievement = _context.Achievements.FirstOrDefault(e => e.Id == id);
            if (achievement == null)
            {
                _logger.LogError("Event not found: {0}", id);
                throw new Exception("Thành tích không tồn tại");
            }

            _context.Achievements.Remove(achievement);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<Paginator<AchievementResponse>> GetAllAsync(int page, int size)
        {
            var achievements = await _context.Achievements
                .Include(e => e.User)
                .Include(e => e.Event)
                .ToListAsync();

            return new Paginator<AchievementResponse>(
                page, size,
                achievements.Count,
                achievements.Skip((page - 1) * size).Take(size)
                .Select(e => e.ToAchievementResponse()).ToList());
        }

        public async Task<Paginator<AchievementResponse>> GetAllWithFilterAsync(
            int page,
            int size,
            string? name = null,
            string? userId = null,
            int? eventId = null,
            DateOnly? startDate = null,
            DateOnly? endDate = null)
        {
            var query = _context.Achievements.AsQueryable();

            if (!string.IsNullOrEmpty(name)) query = query.Where(p => EF.Functions.Like(p.Name, $"%{name}%"));

            if (!string.IsNullOrEmpty(userId)) query = query.Where(f => f.UserId == userId);

            if (eventId.HasValue) query = query.Where(f => f.EventId == eventId.Value);

            if (startDate.HasValue) query = query.Where(f => f.DateAchieved >= startDate.Value);

            if (endDate.HasValue) query = query.Where(f => f.DateAchieved <= endDate.Value);

            var achievements = await query
                .Include(e => e.User)
                .Include(e => e.Event)
                .ToListAsync();

            return new Paginator<AchievementResponse>(
                page, size,
                achievements.Count,
                achievements.Skip((page - 1) * size).Take(size)
                .Select(e => e.ToAchievementResponse()).ToList());
        }

        public async Task<AchievementResponse?> FindByIdAsync(int id)
        {
            var achievement = await _context.Achievements
                .Include(e => e.User)
                .Include(e => e.Event)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (achievement == null)
            {
                _logger.LogError("Achievement not found: {0}", id);
                return null;
            }

            return achievement.ToAchievementResponse();
        }

        public async Task<AchievementResponse?> UpdateAsync(int id, AchievementUpdateSchema schema)
        {
            var achievement = await _context.Achievements
                .Include(e => e.User)
                .Include(e => e.Event)
                .FirstOrDefaultAsync(e => e.Id == id);
            if (achievement == null)
            {
                _logger.LogError("Achievement not found: {0}", id);
                throw new Exception("Thành tích không tồn tại");
            }

            var isValid = await Validate(schema.UserId, schema.EventId);
            if (!string.IsNullOrEmpty(isValid)) throw new ArgumentException(isValid);

            achievement.Name = schema.Name;
            achievement.DateAchieved = schema.DateAchieved;
            achievement.Description = schema.Description;
            achievement.UserId = schema.UserId;
            achievement.EventId = schema.EventId;

            _context.Entry(achievement).State = EntityState.Modified;
            var result = await _context.SaveChangesAsync();

            if (result > 0)
                return await FindByIdAsync(id);

            return null;
        }

        private async Task<string?> Validate(string userId, int? eventId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return "Người dùng không tồn tại";

            if (eventId.HasValue)
            {
                var @event = _context.Events
                    .Include(e => e.EventRegistrations)
                    .FirstOrDefault(e => e.Id == eventId);
                if (@event == null) return "Sự kiện không tồn tại";

                if (!@event.EventRegistrations.Any(er => er.UserId == userId))
                {
                    return "Người dùng chưa đăng ký tham gia sự kiện này";
                }
            }

            return null;
        }
    }
}
