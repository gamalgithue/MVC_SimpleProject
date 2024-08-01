using FirstPro.BLL.Modals;
using FirstPro.DAL.Extend;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FirstPro.Web.Controllers
{

    [Authorize(Roles = "Admin")]

    public class RoleManagementController : Controller
    {
        private readonly RoleManager<ApplicationRole> rolemanager;
        private readonly UserManager<ApplicationUser> usermanager;

        public RoleManagementController(RoleManager<ApplicationRole> _rolemanager,UserManager<ApplicationUser> _usermanager)
        {
            this.rolemanager = _rolemanager;
            this.usermanager = _usermanager;
        }
        public async Task<IActionResult> Index()
        {

            var data = await rolemanager.Roles.ToListAsync();
            return View(data);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(ApplicationRole role)
        {

            role.CreatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);


            var result = await rolemanager.CreateAsync(role);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }

            return View(role);
        }


        [HttpGet]
        public async Task<IActionResult> AddOrRemoveUser(string RoleId)
        {

            var role = await rolemanager.FindByIdAsync(RoleId);

            var model = new List<UserInRoleDTO>();

            foreach (var user in usermanager.Users)
            {
                var userInRole = new UserInRoleDTO()
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await usermanager.IsInRoleAsync(user, role.Name))
                {
                    userInRole.IsSelected = true;
                }
                else
                {
                    userInRole.IsSelected = false;
                }

                model.Add(userInRole);
            }

            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUser(List<UserInRoleDTO> model, string RoleId)
        {
            var role = await rolemanager.FindByIdAsync(RoleId);

            for (int i = 0; i < model.Count; i++)
            {

                var user = await usermanager.FindByIdAsync(model[i].UserId);

                IdentityResult result;

                if (model[i].IsSelected && !(await usermanager.IsInRoleAsync(user, role.Name)))
                {

                    result = await usermanager.AddToRoleAsync(user, role.Name);
                }

                if (!model[i].IsSelected && (await usermanager.IsInRoleAsync(user, role.Name)))
                {
                    result = await usermanager.RemoveFromRoleAsync(user, role.Name);
                }
            }

            return RedirectToAction("Index");
        }


    }
}
