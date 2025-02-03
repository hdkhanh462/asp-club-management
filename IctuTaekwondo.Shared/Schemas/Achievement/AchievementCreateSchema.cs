using System.ComponentModel.DataAnnotations;

namespace IctuTaekwondo.Shared.Schemas.Achievement
{
    public class AchievementCreateSchema
    {
        [Display(Name = "Tên thành tích")]
        [Required(ErrorMessage = "Tên thành tích không được để trống")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Tên thành tích phải có độ dài từ 6 đền 100 ký tự")]
        public string Name { get; set; } = null!;

        [Display(Name = "Ngày đạt")]
        [Required(ErrorMessage = "Ngày đạt không được để trống")]
        [DataType(DataType.Date, ErrorMessage = "Đinh dạng ngày tháng không chính xác")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateOnly DateAchieved { get; set; }

        [Display(Name = "Mô tả")]
        public string? Description { get; set; }

        public string UserId { get; set; } = null!;
        public int? EventId { get; set; }
    }
}
