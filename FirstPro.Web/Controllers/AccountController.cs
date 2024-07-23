using FirstPro.BLL.Helper;
using FirstPro.BLL.Modals;
using FirstPro.DAL.Extend;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FirstPro.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _usermanager;
        private readonly SignInManager<ApplicationUser> _signinmanager;

        public AccountController(UserManager<ApplicationUser> _usermanager,SignInManager<ApplicationUser> _signinmanager)
        {
            this._usermanager = _usermanager;
            this._signinmanager = _signinmanager;
        }

        #region Registration

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public async  Task<IActionResult> Registration(RegistrationDTO model)
        {
            var user = new ApplicationUser()
            {
                UserName = model.FullName,
                Email = model.Email,
                IsAgree=model.IsAgree
            };

            if (ModelState.IsValid)
            {
                var result = await _usermanager.CreateAsync(user, model.Password);
          
                if (result.Succeeded)
                {
                    var token = await _usermanager.GenerateEmailConfirmationTokenAsync(user);

                    var confirmationLink = Url.Action("ConfirmEmail", "Account",
                        new { userId = user.Id, token = token }, Request.Scheme);
                    MailSender.Mail("gemyelbatawy@gmail.com", "Email Confirmation", "Please Confirm Your Email " + confirmationLink);

                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(model);
        }

        #endregion
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("index", "home");
            }

            var user = await _usermanager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"The User ID {userId} is invalid";
                return View("NotFound");
            }

            var result = await _usermanager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return View("Login");
            }

            ViewBag.ErrorTitle = "Email cannot be confirmed";
            return View("Error");
        }

        // rest of the code
    



    #region Login

          [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            var user = await _usermanager.FindByEmailAsync(login.Email);


            if (ModelState.IsValid)
            {
                var result = await _signinmanager.PasswordSignInAsync(user, login.Password, login.RememberMe, false);


                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    ModelState.AddModelError("", "Invalid UserName Or Password");

                }
            }
        

            return View(login);
        }



        #endregion



        #region SignOut
        [HttpPost]
        public async  Task<IActionResult> LogOff()
        {

            await _signinmanager.SignOutAsync();
            return RedirectToAction("Login");

        }
        #endregion


        #region Forget Password

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }


        [HttpGet]
        public IActionResult ConfirmForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordDTO model)
        {
            var user = await _usermanager.FindByEmailAsync(model.Email);

            if (ModelState.IsValid)
            {
                if (user != null)
                {
                    var token = await _usermanager.GeneratePasswordResetTokenAsync(user);

                    var passwordResetLink = Url.Action("ResetPassword", "Account", new { Email = model.Email, Token = token }, Request.Scheme);

                    MailSender.Mail("gemyelbatawy@gmail.com", "ResetPassword", "Please Reset Your Password " + passwordResetLink);


                    //logger.Log(LogLevel.Warning, passwordResetLink);

                    return RedirectToAction("ConfirmForgetPassword");
                }
            }

            return View(model);
            
        }


        #endregion


        #region Reset Password

        [HttpGet]
        public IActionResult ResetPassword(string Email,string Token)
        {
            return View();
        }


        [HttpGet]
        public IActionResult ConfirmResetPassword()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO model)
        {
            var user = await _usermanager.FindByEmailAsync(model.Email);

            if (ModelState.IsValid)
            {
                if (user != null)
                {
                    var result = await _usermanager.ResetPasswordAsync(user, model.Token, model.Password);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("ConfirmResetPassword");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View(model);
                }
            }
            return View(model);


        }

        #endregion

    }
}
