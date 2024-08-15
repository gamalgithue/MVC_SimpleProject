using FirstPro.DAL.Entities;
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

        #region Prop

        private readonly UserManager<ApplicationUser> usermanager;

        #endregion

        #region ctor
        public UserManagementController(UserManager<ApplicationUser> _usermanager)
        {
            this.usermanager = _usermanager;
        }
        #endregion

        #region Actions
        public async Task<IActionResult> Index()
        {
            var data = await usermanager.Users.ToListAsync();
            return View(data);
        }

        
        public async Task<IActionResult> Details(string id)
        {
            var data = await usermanager.Users.FirstOrDefaultAsync(x=>x.Id==id);
            return View(data);
        }



        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var result = await usermanager.Users.FirstOrDefaultAsync(x => x.Id == id);


            return View(result);
        }

        [HttpPost]
       public async Task<IActionResult> Update(ApplicationUser user)
         {
             if (ModelState.IsValid)
               {
                    // Find the existing user by their ID (assuming `Id` is available in the `user` object)
                    var existingUser = await usermanager.FindByIdAsync(user.Id.ToString());

                   if (existingUser != null)
                    {
                        // Update the fields of the existing user with the values from the `user` parameter
                        existingUser.UserName = user.UserName;
                        existingUser.Email = user.Email;
                        // Update other fields as necessary

                        // Update the user in the database
                        var result = await usermanager.UpdateAsync(existingUser);

                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            // Add errors to ModelState if the update failed
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "User not found.");
                    }
                }

                return View(user);
            }


        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await usermanager.Users.FirstOrDefaultAsync(x => x.Id == id);


            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(ApplicationUser user)
        {

            if (ModelState.IsValid)
            {
                // Find the existing user by their ID (assuming `Id` is available in the `user` object)
                var existingUser = await usermanager.FindByIdAsync(user.Id.ToString());

                if (existingUser != null)
                {
                  

                    // Delete the user in the database
                    var result = await usermanager.DeleteAsync(existingUser);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // Add errors to ModelState if the update failed
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User not found.");
                }
            }

            return View(user);
        }

        }

        #endregion
    }


