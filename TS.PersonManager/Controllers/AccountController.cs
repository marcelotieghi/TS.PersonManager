using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TS.PersonManager.Business.Interface;
using TS.PersonManager.Models;

namespace TS.PersonManager.Controllers;

public class AccountController(IValidateUserBusiness business) : Controller
{
    private readonly IValidateUserBusiness _business = business;

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(UserViewModel model, string? returnUrl = null)
    {
        if (!await _business.Validate(model.Username, model.Password))
        {
            ModelState.AddModelError("", "Invalid credentials.");
            return View();
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, model.Username)
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        return Redirect(returnUrl ?? "/Person/Index");
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Login");
    }

}