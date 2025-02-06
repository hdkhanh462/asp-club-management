using System.Web;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace IctuTaekwondo.WebClient.Models
{
    public class ModalViewModel
    {
        public string Id { get; set; }
        public string Header { get; set; }
        public string BodyPartialName { get; set; }
        public object BodyModel { get; set; }
        public ViewDataDictionary? BodyViewData { get; set; }
    }
}
