using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using IctuTaekwondo.Shared.Enums;

namespace IctuTaekwondo.Shared.Responses.Event
{
    public class EventResponse
    {
        public int Id { get; set; }

        [DisplayName("Tên sự kiện")]
        public string Name { get; set; }
        
        [DisplayName("Ngày bắt đầu")]
        public DateTime StartDate { get; set; }
        
        [DisplayName("Ngày kết thúc")]
        public DateTime? EndDate { get; set; }
        
        [DisplayName("Địa điểm tổ chức")]
        public string Location { get; set; }
        
        [DisplayName("Mô tả")]
        public string? Description { get; set; }
        
        [DisplayName("Phí tham gia")]
        public long? Fee { get; set; }
        
        [DisplayName("Giới hạn tham gia")]
        public short? MaxParticipants { get; set; }
        
        [DisplayName("Ngày tạo")]
        public DateTime? CreatedAt { get; set; }
        
        [DisplayName("Trạng thái")]
        public List<EventStatus> Status { get; set; } = [];
    }
}
