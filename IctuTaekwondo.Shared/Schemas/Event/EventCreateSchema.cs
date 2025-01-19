using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IctuTaekwondo.Shared.Schemas.Event
{
    public class TEventWithStartEndDate
    {
        [Display(Name = "Bắt đầu vào")]
        [Column(TypeName = "timestamp without time zone")]
        public DateTime StartDate { get; set; }
        
        [Display(Name = "Kết thúc vào")]
        [Column(TypeName = "timestamp without time zone")]
        [EndDateGreaterThanStartDate]
        public DateTime? EndDate { get; set; }
    }

    public class EndDateGreaterThanStartDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var eventObj = (TEventWithStartEndDate)validationContext.ObjectInstance;
            if (eventObj.EndDate.HasValue && eventObj.EndDate <= eventObj.StartDate)
            {
                return new ValidationResult("Ngày kết thúc phải lớn hơn ngày bắt đầu.");
            }
            return ValidationResult.Success!;
        }
    }

    public class EventCreateSchema : TEventWithStartEndDate
    {
        [MaxLength(100)]
        public string Name { get; set; }

        public string Location { get; set; }

        public string? Description { get; set; }

        public int Fee { get; set; }

        public int? MaxParticipants { get; set; }
    }
}
