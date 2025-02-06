using IctuTaekwondo.WebClient.Models;
using Microsoft.AspNetCore.Mvc;

namespace IctuTaekwondo.WebClient.Components
{
    public class ModalViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ModalViewModel model)
        {
            return View(model);
        }
    }
}
