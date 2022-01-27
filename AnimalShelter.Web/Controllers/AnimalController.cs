using Microsoft.AspNetCore.Mvc;

namespace AnimalShelter.Web.Controllers
{
    public class AnimalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
