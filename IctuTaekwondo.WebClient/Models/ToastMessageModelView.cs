using IctuTaekwondo.Shared.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace IctuTaekwondo.WebClient.Models
{
    public class ToastMessageModelView
    {
        public string Id { get; set; }
        public ToastType Type { get; set; }
        public string Message { get; set; }
        public ModelErrorCollection Errors { get; set; } = [];

        public string GetHtmlId() 
        { 
            return $"Toast-{Id}";
        }
    }
}
