using IctuTaekwondo.Shared.Responses;
using Microsoft.AspNetCore.Http.Extensions;
using IctuTaekwondo.Shared.Responses.Achievement;
using IctuTaekwondo.Shared.Schemas.Achievement;

namespace IctuTaekwondo.Shared.Services.Achievements
{
    public interface IAchievementsService
    {
        public Task<PaginationResponse<AchievementResponse>> GetAllAsync(string token, int page, int size, QueryBuilder? query = null);
        public Task<AchievementResponse?> FindByIdAsync(string token, int id);
        public Task<AchievementResponse?> CreateAsync(string token, AchievementCreateSchema schema);
        public Task<AchievementResponse?> UpdateAsync(string token, int id, AchievementUpdateSchema schema);
        public Task<bool> DeleteAsync(string token, int id);
    }
}
