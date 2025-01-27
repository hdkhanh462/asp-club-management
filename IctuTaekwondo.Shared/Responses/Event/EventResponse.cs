using System.ComponentModel.DataAnnotations;

namespace IctuTaekwondo.Shared.Responses.Event
{
    public class EventResponse
    {
        public int Id { get; set; }

        [Display(Name = "Tên sự kiện")]
        public string Name { get; set; }

        [Display(Name = "Ngày bắt đầu")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Display(Name = "Ngày kết thúc")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Địa điểm")]
        public string Location { get; set; }

        [Display(Name = "Mô tả")]
        public string? Description { get; set; }

        [Display(Name = "Phí tham gia")]
        public int Fee { get; set; }

        [Display(Name = "Giới hạn tham gia")]
        public int? MaxParticipants { get; set; }

        [Display(Name = "Ngày tạo")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? CreatedAt { get; set; }
    }
}
