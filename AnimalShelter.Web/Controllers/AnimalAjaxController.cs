using AnimalShelter.Domain;
using AnimalShelter.Web.ApplicationServices;
using AnimalShelter.Web.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] AnimalSaveRequest animal)
        {
            if(animal == null)
            {
                return Json(new
                {
                    Success = false,
                    Errors = "Animal request was empty"
                });
            }
            var saveAnimalResult = await _animalApplicationService.Save(animal);

            if (saveAnimalResult.IsFailure)
            {
                return Json(new
                {
                    Success = false,
                    Errors = saveAnimalResult.Error
                });
            }

            return Json(new
            {
                Success = true
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] AnimalUpdateRequest animal)
        {
            if (animal == null)
            {
                return Json(new
                {
                    Success = false,
                    Errors = "Animal request was empty"
                });
            }

            var updateAnimalResult = await _animalApplicationService.Update(animal);

            if (updateAnimalResult.IsFailure)
            {
                return Json(new
                {
                    Success = false,
                    Errors = updateAnimalResult.Error
                });
            }

            return Json(new
            {
                Success = true
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] Guid id)
        {
            var guidResult = NonEmptyGuid.Create(id);
            if (guidResult.IsFailure)
            {
                return Json(new
                {
                    Success = false,
                    Errors = "Id of animal sent was empty or null"
                });
            }

            var deleteAnimalResult = await _animalApplicationService.Delete(id);
            
            if (deleteAnimalResult.IsFailure)
            {
                return Json(new
                {
                    Success = false,
                    Errors = deleteAnimalResult.Error
                });
            }

            return Json(new
            {
                Success = true
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetOne(Guid id)
        {
            var guidResult = NonEmptyGuid.Create(id);
            if (guidResult.IsFailure)
            {
                return Json(new
                {
                    Success = false,
                    Errors = "Id of animal sent was empty or null"
                });
            }

            var getOneAnimalResult = await _animalApplicationService.GetById(id);

            if (getOneAnimalResult.IsFailure)
            {
                return Json(new
                {
                    Success = false,
                    Errors = getOneAnimalResult.Error
                });
            }

            return Json(new
            {
                Success = true
            });
        }
    }
}
