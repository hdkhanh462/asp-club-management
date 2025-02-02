using System.ComponentModel.DataAnnotations;
using IctuTaekwondo.Shared.DataAnnotations;

namespace IctuTaekwondo.Shared.Schemas.Event
{
    public class TEventWithStartEndDate
    {
        [Display(Name = "Bắt đầu vào")]
        [DataType(DataType.DateTime, ErrorMessage = "Định dạng ngày tháng không chính xác")]
        public DateTime StartDate { get; set; }
        
        [Display(Name = "Kết thúc vào")]
        [DataType(DataType.DateTime, ErrorMessage = "Định dạng ngày tháng không chính xác")]
        [EndDateGreaterThanStartDate]
        public DateTime? EndDate { get; set; }
    }

    public class EventCreateSchema : TEventWithStartEndDate
    {
        [Display(Name = "Tên sự kiện")]
        [Required(ErrorMessage = "Trường bắt buộc")]
        [StringLength(100)]
        public string Name { get; set; }

        [Display(Name = "Địa điểm")]
        [Required(ErrorMessage = "Trường bắt buộc")]
        public string Location { get; set; }

        [Display(Name = "Mô tả")]
        public string? Description { get; set; }

        [Display(Name = "Phí tham gia")]
        [Required(ErrorMessage = "Trường bắt buộc")]
        public int Fee { get; set; }

        [Display(Name = "Giới hạn tham gia")]
        public int? MaxParticipants { get; set; }
    }
}
