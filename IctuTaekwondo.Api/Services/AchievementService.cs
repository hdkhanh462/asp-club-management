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
        Task<bool> CreateAsync(AchievementCreateSchema schema);
        Task<bool> UpdateAsync(int id, AchievementUpdateSchema schema);
        Task<bool> DeleteAsync(int id);
        Task<PaginationResponse<AchievementResponse>> GetAllAsync(int page, int size);
        Task<AchievementResponse?> GetByIdAsync(int id);
        Task<PaginationResponse<AchievementResponse>> GetAllWithFilterAsync(
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

        public async Task<bool> CreateAsync(AchievementCreateSchema schema)
        {
            var user = await _userManager.FindByIdAsync(schema.UserId);
            if (user == null)
            {
                _logger.LogError("User not found: {0}", schema.UserId);
                throw new Exception("Người dùng không tồn tại");
            }

            var @event = _context.Events.FirstOrDefault(e => e.Id == schema.EventId);
            if (@event == null)
            {
                _logger.LogError("Event not found: {0}", schema.EventId);
                throw new Exception("Sự kiện không tồn tại");
            }

            var newAchievement = new Achievement
            {
                Name = schema.Name,
                DateAchieved = schema.DateAchieved,
                Description = schema.Description,
                CreatedAt = DateTime.UtcNow,
                UserId = user.Id,
                EventId = @event.Id,
            };

            _context.Achievements.Add(newAchievement);
            var result = await _context.SaveChangesAsync();

            return result > 0;
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

        public async Task<PaginationResponse<AchievementResponse>> GetAllAsync(int page, int size)
        {
            var achievements = await _context.Achievements
                .Include(e=> e.User)
                .Include(e=> e.Event)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return new PaginationResponse<AchievementResponse>(achievements.Count, size)
            {
                CurrentPage = page,
                Items = achievements.Select(e => e.ToAchievementResponse()).ToList()
            };
        }

        public async Task<PaginationResponse<AchievementResponse>> GetAllWithFilterAsync(
            int page, 
            int size, 
            string? name = null, 
            string? userId = null, 
            int? eventId = null,
            DateOnly? startDate = null,
            DateOnly? endDate = null)
        {
            var query = _context.Achievements.AsQueryable();

            if (!string.IsNullOrEmpty(name)) query = query.Where(f => f.Name == name);

            if (startDate.HasValue) query = query.Where(f => f.DateAchieved >= startDate.Value);

            if (endDate.HasValue) query = query.Where(f => f.DateAchieved <= endDate.Value);

            var achievements = await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return new PaginationResponse<AchievementResponse>(achievements.Count, size)
            {
                CurrentPage = page,
                Items = achievements.Select(e => e.ToAchievementResponse()).ToList()
            };
        }

        public async Task<AchievementResponse?> GetByIdAsync(int id)
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

        public async Task<bool> UpdateAsync(int id, AchievementUpdateSchema schema)
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

            var user = await _userManager.FindByIdAsync(schema.UserId);
            if (user == null)
            {
                _logger.LogError("User not found: {0}", schema.UserId);
                throw new Exception("Người dùng không tồn tại");
            }

            var @event = _context.Events.FirstOrDefault(e => e.Id == schema.EventId);
            if (@event == null)
            {
                _logger.LogError("Event not found: {0}", schema.EventId);
                throw new Exception("Sự kiện không tồn tại");
            }

            achievement.Name = schema.Name;
            achievement.DateAchieved = schema.DateAchieved;
            achievement.Description = schema.Description;
            achievement.UserId = user.Id;
            achievement.EventId = @event.Id;

            _context.Entry(achievement).State = EntityState.Modified;
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }
    }
}
