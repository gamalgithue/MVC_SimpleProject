using FirstPro.BLL.Helper;
using FirstPro.BLL.Modals;
using FirstPro.DAL.Extend;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Claims;

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
                    MailSender.Mail(user.Email, "Email Confirmation", "Please Confirm Your Email " + confirmationLink);

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
                return View();
            }

            ViewBag.ErrorTitle = "Email cannot be confirmed";
            return View("Error");
        }
       

        // rest of the code
    



    #region Login

          [HttpGet]
        public async Task<IActionResult> Login(string returnUrl="")
        {

            LoginDTO login = new LoginDTO
            {
                ReturnUrl = returnUrl,
                ExternalLogins =
        (await _signinmanager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            return View(login);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO login,string returnUrl="")
        {


           

            
                var user = await _usermanager.FindByEmailAsync(login.Email);

                var result = await _signinmanager.PasswordSignInAsync(user, login.Password, login.RememberMe, false);


                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    ModelState.AddModelError("", "Invalid UserName Or Password");

                }
            
        

            return View(login);
        }


        [AllowAnonymous]
        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account",
                                new { ReturnUrl = returnUrl });
            var properties = _signinmanager
                .ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl=null, string remoteError=null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            LoginDTO model = new LoginDTO
            {
                ReturnUrl = returnUrl,
                ExternalLogins =
                        (await _signinmanager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if (remoteError != null)
            {
                ModelState
                    .AddModelError(string.Empty, $"Error from external provider: {remoteError}");

                return View("Login", model);
            }

            // Get the login information about the user from the external login provider
            var info = await _signinmanager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ModelState
                    .AddModelError(string.Empty, "Error loading external login information.");

                return View("Login", model);
            }
            // If the user already has a login (i.e if there is a record in AspNetUserLogins
            // table) then sign-in the user with this external login provider
            var signInResult = await _signinmanager.ExternalLoginSignInAsync(info.LoginProvider,
                info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            if (signInResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            // If there is no record in AspNetUserLogins table, the user may not have
            // a local account
            else
            {
                // Get the email claim value
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);

                if (email != null)
                {
                    // Create a new user without password if we do not have a user already
                    var user = await _usermanager.FindByEmailAsync(email);

                    if (user == null)
                    {
                        user = new ApplicationUser
                        {
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                        };

                        await _usermanager.CreateAsync(user);
                        //var token = await _usermanager.GenerateEmailConfirmationTokenAsync(user);

                        //var confirmationLink = Url.Action("ConfirmEmail", "Account",
                        //                new { userId = user.Id, token = token }, Request.Scheme);
                        //MailSender.Mail("gemyelbatawy@gmail.com", "Email Confirmation", "Please Confirm Your Email " + confirmationLink);

                    }
                    // Add a login (i.e insert a row for the user in AspNetUserLogins table)
                    await _usermanager.AddLoginAsync(user, info);
                    await _signinmanager.SignInAsync(user, isPersistent: false);

                    return LocalRedirect(returnUrl);
                }

                // If we cannot find the user email we cannot continue
                ViewBag.ErrorTitle = $"Email claim not received from: {info.LoginProvider}";
                ViewBag.ErrorMessage = "Please contact support on Pragim@PragimTech.com";

                return View("Error");
            }
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

                    MailSender.Mail(user.Email, "ResetPassword", "Please Reset Your Password " + passwordResetLink);


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
        public IActionResult AccessDenied()
        {
            return RedirectToAction("Login");
        }

        #endregion

    }
}
