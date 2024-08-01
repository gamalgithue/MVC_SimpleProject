using FirstPro.DAL.Extend;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstPro.Web.Controllers
{

    [Authorize(Roles = "Admin")]

    public class UserManagementController : Controller
    {
        private readonly UserManager<ApplicationUser> usermanager;

        public UserManagementController(UserManager<ApplicationUser> _usermanager)
        {
            this.usermanager = _usermanager;
        }
        public async  Task<IActionResult> Index()
        {
            var data = await usermanager.Users.ToListAsync();
            return View(data);
        }
    }
}
