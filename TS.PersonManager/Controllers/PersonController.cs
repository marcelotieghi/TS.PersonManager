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
            return PartialView("_CreateEditPartial", new PersonViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(PersonViewModel model, CancellationToken token)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                if (model is null)
                    return BadRequest("Invalid person data");

                if (model.ImageUpload != null)
                {
                    var imagesPath = Path.Combine("wwwroot", "images");
                    if (!Directory.Exists(imagesPath))
                        Directory.CreateDirectory(imagesPath);

                    var filePath = Path.Combine(imagesPath, model.ImageUpload.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ImageUpload.CopyToAsync(stream, token);
                    }

                    model.ImagePath = $"/images/{model.ImageUpload.FileName}";
                }

                await _business.CreateModify(model);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred: " + ex.Message;
                return View(model);
            }
        }
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
        public async Task<IActionResult> Update(PersonViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

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