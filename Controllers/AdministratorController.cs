namespace dotnet_mvc.Controllers
{
    public class AdministratorController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public UserManager<ApplicationUser> UserManager;

        public AdministratorController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.UserManager = userManager;
        }

        [HttpGet] 
        public IActionResult CreateRole() {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var identityRole = new IdentityRole
                {
                    Name = model.role
                };
                var result = await roleManager.CreateAsync(identityRole);
                Console.WriteLine("AA" + result.Succeeded);
                if (result.Succeeded)
                {
                    return RedirectToAction("GetRoles", "Administrator");
                }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
            return View(model);
        }

        public IActionResult GetRoles()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if(role == null)
            {
                ViewBag.Msg = $"No Role found of ID: {id}";
                return View("NotFound");

            }
            else
            {
                var model = new EditRoleViewModel
                {
                    id = id,
                    role = role.Name
                };
                foreach(var user in UserManager.Users)
                {
                    if(await UserManager.IsInRoleAsync(user, role.Name))
                    {
                        model.users.Add(user);
                    }
                }
                return View(model);
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.id);
            if(role == null)
            {
                ViewBag.Msg = $"No Role found of ID: {model.id}";
                return View("NotFound");

            }
            else
            {
                role.Name = model.role; 
                var result = await roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("GetRoles", "Administrator");
                }
                else
                {
                    foreach(var err in  result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                    return View(model);
                }
            }
        }
    }
}
