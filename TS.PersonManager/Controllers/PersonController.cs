using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TS.PersonManager.Business.Interface;
using TS.PersonManager.Business.ViewModels;

namespace TS.PersonManager.Controllers
{
    [Authorize]
    public class PersonController(IPersonBusiness business) : Controller
    {
        private readonly IPersonBusiness _business = business;

        public async Task<IActionResult> Index()
        {
            try
            {
                var models = await _business.GetAll();
                return View(models);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred: " + ex.Message;
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var model = await _business.GetById(id);

                if (model is null)
                    return NotFound("Person not found");

                return PartialView("_PersonDetailsPartial", model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new PersonViewModel { DateOfBirth = DateTime.Today };
            return PartialView("_CreateEditPartial", model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var model = await _business.GetById(id);

                if (model is null)
                {
                    return NotFound("Person not found");
                }
                return PartialView("_CreateEditPartial", model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateModify(PersonViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                if (model is null)
                    return BadRequest("Invalid person data");

                await _business.CreateModify(model);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred: " + ex.Message;
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var model = await _business.GetById(id);

                if (model is null)
                    return NotFound("Person not found");

                await _business.Remove(id);

                return Ok();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred: " + ex.Message;
                return StatusCode(500, "Internal server error");
            }
        }
    }
}