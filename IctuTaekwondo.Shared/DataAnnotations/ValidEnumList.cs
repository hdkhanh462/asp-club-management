using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace IctuTaekwondo.Shared.DataAnnotations
{
    public class ValidEnumList : ValidationAttribute
    {
        public Type EnumType { get; set; } = null!;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is not IEnumerable enumerable)
            {
                return new ValidationResult(ErrorMessage);
            }

            var validValues = Enum.GetValues(EnumType).Cast<object>().ToHashSet();

            foreach (var item in enumerable)
            {
                if (item == null || !validValues.Contains(item))
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}
