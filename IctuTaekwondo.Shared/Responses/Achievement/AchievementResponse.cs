using System.ComponentModel.DataAnnotations;
using IctuTaekwondo.Shared.Responses.User;
using IctuTaekwondo.Shared.Responses.Event;
using System.ComponentModel;

namespace IctuTaekwondo.Shared.Responses.Achievement
{
    public class AchievementResponse
    {
        public int Id { get; set; }

        [DisplayName("Tên thành tích")]
        public string Name { get; set; }

        [DisplayName("Ngày đạt")]
        public DateOnly DateAchieved { get; set; }

        [DisplayName("Mô tả")]
        public string? Description { get; set; }

        [DisplayName("Ngày tạo")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? CreatedAt { get; set; }

        public virtual UserResponse User { get; set; }
        public virtual EventResponse? Event { get; set; }

    }
}
