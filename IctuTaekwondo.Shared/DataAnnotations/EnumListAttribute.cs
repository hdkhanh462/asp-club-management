using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace IctuTaekwondo.Shared.DataAnnotations
{
    public class EnumListAttribute : ValidationAttribute
    {
        private readonly Type _enumType;

        public EnumListAttribute(Type enumType)
        {
            _enumType = enumType;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is not IEnumerable enumerable)
            {
                return new ValidationResult(ErrorMessage);
            }

            var validValues = Enum.GetValues(_enumType).Cast<object>().ToHashSet();

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
