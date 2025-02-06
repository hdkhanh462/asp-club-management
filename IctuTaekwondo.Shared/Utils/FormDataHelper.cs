using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IctuTaekwondo.Shared.Utils
{
    public static class FormDataHelper
    {
        public static MultipartFormDataContent ToMultipartFormDataContent<T>(this T model)
        {
            
            var formData = new MultipartFormDataContent();

            foreach (var property in typeof(T).GetProperties())
            {
                var propertyValue = property.GetValue(model);

                if (propertyValue is IFormFile file)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        file.CopyTo(memoryStream);
                        var byteArrayContent = new ByteArrayContent(memoryStream.ToArray());
                        formData.Add(byteArrayContent, file.Name, file.FileName);
                    }
                }
                else if (propertyValue is IEnumerable propertyValues && propertyValue is not string)
                {
                    foreach (var value in propertyValues)
                    {
                        formData.Add(new StringContent(value.ToString() ?? string.Empty), property.Name);
                    }
                }
                else if (propertyValue != null)
                {
                    formData.Add(new StringContent(propertyValue.ToString() ?? string.Empty), property.Name);
                }
            }

            return formData;
        }

        public static StringContent ToStringContent<T>(this T model)
        {
            return new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
        }
    }
}
