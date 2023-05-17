using System.Data;

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

        [HttpGet]
        public async Task<IActionResult> EditUserInRole(string id)
        {
            ViewBag.role = id;

            var role = await roleManager.FindByIdAsync(id);
            if(role == null)
            {
                ViewBag.Msg = $"Role with ID: {id} is not found...";
                return RedirectToAction("NotFound");
            }
            var model = new List<EditUserInRoleViewModel>();

            foreach(var user in UserManager.Users)
            {
                var userModel = new EditUserInRoleViewModel
                {
                    UserId = user.Id,
                    userName = user.UserName
                };

                if(await UserManager.IsInRoleAsync(user, role.Name)){
                    userModel.isSelected = true;
                }
                else
                {
                    userModel.isSelected = false;
                }
                model.Add(userModel);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserInRole(List<EditUserInRoleViewModel> model, string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.Msg = $"Role with ID: {id} is not found...";
                return RedirectToAction("NotFound");
            }

            for (var i = 0; i < model.Count; i++)
            {
                var user = await UserManager.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;
                
                if (model[i].isSelected && !(await UserManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await UserManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].isSelected && await UserManager.IsInRoleAsync(user, role.Name))
                {
                    result  = await UserManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if(i == (model.Count - 1))
                    {
                        return RedirectToAction("EditRole", new { id = id });
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    foreach(var err in result.Errors)
                    {

                        ModelState.AddModelError("", err.Description);
                    }
                }
            }
            return RedirectToAction("EditRole", new { id = id});

        }

    }
}
