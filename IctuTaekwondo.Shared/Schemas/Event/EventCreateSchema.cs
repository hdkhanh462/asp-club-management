using System.ComponentModel.DataAnnotations;
using IctuTaekwondo.Shared.DataAnnotations;

namespace IctuTaekwondo.Shared.Schemas.Event
{
    public class EventCreateSchema
    {
        [Display(Name = "Tên sự kiện")]
        [Required(ErrorMessage = "Tên sự kiện không được để trống")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Tên sự kiện phải có độ dài từ 2 đến 100 ký tự")]
        public string Name { get; set; }

        [Display(Name = "Địa điểm")]
        [Required(ErrorMessage = "Địa điểm không được để trống")]
        public string Location { get; set; }

        [Display(Name = "Bắt đầu vào")]
        [DataType(DataType.DateTime, ErrorMessage = "Định dạng ngày giờ không chính xác")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Display(Name = "Kết thúc vào")]
        [DataType(DataType.DateTime, ErrorMessage = "Định dạng ngày giờ không chính xác")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [GreaterThan("StartDate", ErrorMessage = "Ngày kết thúc phải lớn hơn Ngày bắt đầu")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Mô tả")]
        public string? Description { get; set; }

        [Display(Name = "Phí tham gia")]
        [Range(1000, long.MaxValue, ErrorMessage = "Phí tham gia phải có giá trị lớn hơn hoặc bằng 1000")]
        public long? Fee { get; set; }

        [Display(Name = "Giới hạn tham gia")]
        [Range(1, 999, ErrorMessage = "Giới hạn tham gia phải lớn hơn hoặc bằng 1 và nhỏ hơn hoặc bằng 999")]
        public short? MaxParticipants { get; set; }
    }
}
