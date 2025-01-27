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

        [Display(Name = "Tên thành tích")]
        public string Name { get; set; }

        [Display(Name = "Ngày đạt")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateOnly DateAchieved { get; set; }

        [Display(Name = "Mô tả")]
        public string? Description { get; set; }

        [Display(Name = "Ngày tạo")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? CreatedAt { get; set; }

        public virtual UserResponse User { get; set; }
        public virtual EventResponse? Event { get; set; }

    }
}
