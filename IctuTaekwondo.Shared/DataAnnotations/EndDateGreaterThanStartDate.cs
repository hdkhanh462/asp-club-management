using System.ComponentModel.DataAnnotations;
using IctuTaekwondo.Shared.Schemas.Event;

namespace IctuTaekwondo.Shared.DataAnnotations
{
    public class EndDateGreaterThanStartDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var eventObj = (TEventWithStartEndDate)validationContext.ObjectInstance;
            if (eventObj.EndDate.HasValue && eventObj.EndDate <= eventObj.StartDate)
            {
                return new ValidationResult("Thời điểm kết thúc phải lớn hơn thời điểm bắt đầu.");
            }
            return ValidationResult.Success!;
        }
    }
}
