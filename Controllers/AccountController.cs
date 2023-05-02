﻿

using Microsoft.AspNetCore.Identity;

namespace dotnet_mvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            Console.WriteLine("LAYOUT" + signInManager.IsSignedIn(User));
            await signInManager.SignOutAsync(); 
            return RedirectToAction("allEmployees", "home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.email, Email = model.email };
                var result = await userManager.CreateAsync(user, model.password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("allEmployees", "home");
                }
                foreach(var err in result.Errors)
                {
                    ModelState.AddModelError("", $"Error when creating or signing user: {err.Description} with code: {err.Code}");
                }
            }
            return View();
        }
    }
}