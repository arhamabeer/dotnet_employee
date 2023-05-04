

using dotnet_mvc.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace dotnet_mvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<bool> IsEmailInUse(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            Console.WriteLine("IsEmailInUse");
            if (user == null)
            {
                Console.WriteLine("IsEmailInUse IF" + user);
                return true;

            }
            else
            {
                Console.WriteLine("IsEmailInUse ELSE" + user);
                return false;
            }
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            Console.WriteLine("LAYOUT" + signInManager.IsSignedIn(User));
            await signInManager.SignOutAsync(); 
            return RedirectToAction("login", "account");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var check = await IsEmailInUse(model.email);
                if (check)
                {
                    var user = new ApplicationUser { UserName = model.email, Email = model.email, city = model.city };
                    var result = await userManager.CreateAsync(user, model.password);
                    if (result.Succeeded)
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("allEmployees", "home");
                    }
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError("", $"Error when creating or signing user: {err.Description} with code: {err.Code}");
                    }
                }
                else
                {
                    ModelState.AddModelError("", $"Email: {model.email} is already in use");
                }
            }
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model,string? ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.email, model.password, model.rememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                    return RedirectToAction("login", "account");
                    }
                }
                ModelState.AddModelError("", $"Failed to Login");
                
            }
            return View();
        }

       
    }
}
