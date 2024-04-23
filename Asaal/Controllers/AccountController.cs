using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Asaal.Models;
using Asaal.ViewModels;
using System.Data.Entity.Validation;
using Asaal.Resources;
using System.Configuration;

namespace Asaal.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationDbContext _context= new ApplicationDbContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        [AllowAnonymous]
        public ActionResult UserProfile(string id, bool? infoState, bool? photoState, bool? changePasswordState)
        {
            var userInDb = _context.Users.Find(id);
            if (userInDb == null)
                return HttpNotFound();

            var viewModel = new UserProfileVM
            {
                IsSupervisor = UserManager.IsInRole(id, RoleName.Supervisors),
                IsAdmin = UserManager.IsInRole(id, RoleName.Admins),
                User = userInDb,
                ChangePassword = new ChangePasswordViewModel(),
                UpdateProfile = new UpdateProfile
                {
                    CountryCode = userInDb.CountryCode,
                    FullName = userInDb.FullName
                }
            };

            ViewBag.updateUserInfoIsDone = infoState;
            ViewBag.changePhotoIsDone = photoState;
            ViewBag.changePasswordIsDone = changePasswordState;

            return View(viewModel);
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);

                case SignInStatus.LockedOut:
                    return View("Lockout");

                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });

                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", ViewsKeys.incorrectEmailOrPasswordMsg);
                    return View(model);
            }
        }

        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Questions");
        }



        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,

                    CountryCode = model.CountryCode,
                    FullName = model.FullName,
                    ProfileImgSrc = "/Content/user.jpg"
                };

                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);


                    var sendEmailOrNot = ConfigurationManager.AppSettings["sendEmailOrNot"].ToString();
                    if (sendEmailOrNot == "true")
                    {
                        // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);

                        var siteDomain = ConfigurationManager.AppSettings["siteDomain"].ToString();
                        string msgSubject = ViewsKeys.accountConfirmationEmailSubject + " | " + ViewsKeys.appName;
                        string msgBody = @"
                                        <div class='email'>
                                            <div class='header'>
                                                <h2 style='color: #222;'>" + ViewsKeys.hello + model.FullName + @" </h2>
                                            </div>

                                            <div class='body'>
                                                <p style='
                                                    color: #222;
                                                    text-align: center;
                                                    font-size: 18px'>
                                                     " + ViewsKeys.accountConfirmationEmailbBodyP + @"
                                                </p>

                                                <a style='text-align: center;
                                                    text-decoration: none;
                                                    background-color: #2EBAEE;
                                                    color: #fff;
                                                    padding: 7px 15px;
                                                    border: 0;
                                                    border-radius: 8px;
                                                    font-size: 18px;
                                                    font-weight: bold;
                                                    display: block;
                                                    margin: auto;
                                                    width: 150px;
                                                    text-transform: capitalize;' href= " + callbackUrl + @">" + ViewsKeys.confirmEmail + @"</a>
                                            </div>
                                                        
                                            <div class='footer'>
                                                <p style='color: #222;
                                                    text-align: center;
                                                    font-size: 14px;
                                                    margin: 40px 10px 20px;
                                                    border-top: 2px solid #ddd;
                                                    padding-top: 20px;'>
                                                    " + ViewsKeys.accountConfirmationEmailFooterP1 + @"<a href=" + siteDomain + @">" + ViewsKeys.appName + @"</a>, " + ViewsKeys.accountConfirmationEmailFooterP2 + @"
                                                </p>
                                            </div>

                                        </div>
                                    ";

                        await UserManager.SendEmailAsync(user.Id, msgSubject, msgBody);
                    }

                    return RedirectToAction("Index", "Questions");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    ModelState.AddModelError("", ViewsKeys.faildToResetPasswordErrorMsg);
                    return View();
                }

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);

                var siteDomain = ConfigurationManager.AppSettings["siteDomain"].ToString();
                string msgSubject = ViewsKeys.resetPasswordEmailSubject + " | " + ViewsKeys.appName;
                string msgBody = @"
                                        <div class='email'>
                                            <div class='header'>
                                                <h2 style='color: #222;'>" + ViewsKeys.hello + user.FullName + @" </h2>
                                            </div>

                                            <div class='body'>
                                                <p style='
                                                    color: #222;
                                                    text-align: center;
                                                    font-size: 18px'>
                                                     " + ViewsKeys.resetPasswordEmailbBodyP + @"
                                                </p>

                                                <a style='text-align: center;
                                                    text-decoration: none;
                                                    background-color: #2EBAEE;
                                                    color: #fff;
                                                    padding: 7px 15px;
                                                    border: 0;
                                                    border-radius: 8px;
                                                    font-size: 18px;
                                                    font-weight: bold;
                                                    display: block;
                                                    margin: auto;
                                                    width: 150px;
                                                    text-transform: capitalize;' href= " + callbackUrl + @">" + ViewsKeys.resetPassword + @"</a>
                                            </div>
                                                        
                                            <div class='footer'>
                                                <p style='color: #222;
                                                    text-align: center;
                                                    font-size: 14px;
                                                    margin: 40px 10px 20px;
                                                    border-top: 2px solid #ddd;
                                                    padding-top: 20px;'>
                                                    " + ViewsKeys.resetPasswordEmailFooterP1 + @"<a href=" + siteDomain + @">" + ViewsKeys.appName + @"</a>, " + ViewsKeys.resetPasswordEmailFooterP2 + @"
                                                </p>
                                            </div>

                                        </div>
                                    ";

                await UserManager.SendEmailAsync(user.Id, msgSubject, msgBody);

                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }





        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Questions");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}