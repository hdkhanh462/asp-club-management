using System.ComponentModel.DataAnnotations;
using IctuTaekwondo.Shared.Responses.User;
using IctuTaekwondo.Shared.Responses.Event;

namespace IctuTaekwondo.Shared.Responses.Achievement
{
    public class AchievementResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateOnly DateAchieved { get; set; }

        public string? Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? CreatedAt { get; set; }

        public virtual UserResponse User { get; set; }
        public virtual EventResponse? Event { get; set; }

    }
}
