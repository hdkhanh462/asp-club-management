using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace IctuTaekwondo.Shared.DataAnnotations
{
    public class FileUploadAttribute : ValidationAttribute
    {
        public int MaxFileSizeMb { get; set; } = 1;
        public string[] AllowedFileExtentions { get; set; } = [".jpg", ".jpeg", ".png",];

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = (IFormFile)value;

            if (file == null) return ValidationResult.Success!;

            var maxFileSizeByte = MaxFileSizeMb * 1024 * 1024;
            var fileSize = file.Length / 1024 / 1024;

            var fileExtention = Path.GetExtension(file.FileName)?.ToLowerInvariant();

            if (file.Length > maxFileSizeByte)
            {
                return new ValidationResult($"Kích thước tệp không được vượt quá {MaxFileSizeMb}Mb. " +
                    $"Kích thước hiện tại: {Math.Round((decimal)fileSize, 2)}Mb");
            }

            if (!AllowedFileExtentions.Contains(fileExtention))
            {
                return new ValidationResult($"Định dạng '{fileExtention}' không cho phép. " +
                    $"Vui lòng sử dụng các định dạng file cho phép sau: {string.Join(" | ", AllowedFileExtentions)}");
            }

            return ValidationResult.Success!;
        }
    }
}
