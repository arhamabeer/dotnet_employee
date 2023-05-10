namespace dotnet_mvc.Controllers
{
    public class AdministratorController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public AdministratorController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
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
    }
}
