using IctuTaekwondo.Api.Models;
using IctuTaekwondo.Shared.Responses.Achievement;
using IctuTaekwondo.Shared.Responses.Event;

namespace IctuTaekwondo.Api.Mappers
{
    public static class AchievementsMapper
    {
        public static AchievementResponse ToAchievementResponse(this Achievement achievement)
        {
            return new AchievementResponse
            {
                Id = achievement.Id,
                Name = achievement.Name,
                DateAchieved = achievement.DateAchieved,
                Description = achievement.Description,
                CreatedAt = achievement.CreatedAt,
                User = achievement.User.ToUserResponse(),
                Event = achievement.Event is not null ? achievement.Event.ToEventResponse() : null,
            };
        }
    }
}
