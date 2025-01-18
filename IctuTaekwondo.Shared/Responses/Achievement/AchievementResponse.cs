using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using IctuTaekwondo.Shared.Responses.User;
using IctuTaekwondo.Shared.Responses.Event;

namespace IctuTaekwondo.Shared.Responses.Achievement
{
    public class AchievementResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly DateAchieved { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public virtual UserResponse User { get; set; }
        public virtual EventResponse? Event { get; set; }

    }
}
