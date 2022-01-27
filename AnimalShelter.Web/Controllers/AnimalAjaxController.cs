using AnimalShelter.Web.ApplicationServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AnimalShelter.Web.Controllers
{
    public class AnimalAjaxController : Controller
    {
        private readonly AnimalApplicationService _animalApplicationService;

        public AnimalAjaxController(AnimalApplicationService AnimalApplicationService)
        {
            _animalApplicationService = AnimalApplicationService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var animalResult = await _animalApplicationService.GetAll();

            return Json(new
            {
                Success = true,
                Model = animalResult.Value
            });
        }
    }
}
