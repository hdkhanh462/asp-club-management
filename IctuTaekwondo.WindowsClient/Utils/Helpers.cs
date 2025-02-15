using IctuTaekwondo.Shared.Enums;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace IctuTaekwondo.WindowsClient.Utils
{
    public class Helpers
    {
        public static void LoadComboBoxs<T>(ComboBox control, string? alias = null) where T : Enum
        {
            var values = Enum
                .GetValues(typeof(T))
                .Cast<T>()
                .Select(g => new { Value = g, DisplayName = g.GetDisplayName() })
                .ToList();

            //if (!string.IsNullOrEmpty(alias)) 
            //    values.Insert(0, new { Value = default(T), DisplayName = $"Chọn {alias}" });

            control.DataSource = values;
            control.DisplayMember = "DisplayName";
            control.ValueMember = "Value";
        }

        public static IFormFile ConvertToIFormFile(string filePath)
        {
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var formFile = new FormFile(fileStream, 0, fileStream.Length, "Image", Path.GetFileName(filePath))
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/" + Path.GetExtension(filePath).TrimStart('.')
            };

            return formFile;
        }

        public static bool IsValidSchema(object schema)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(schema);

            if (!Validator.TryValidateObject(schema, context, results, true))
            {
                MessageBox.Show(string.Join("\n", results.Select(x => x.ErrorMessage)), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
    }
}
