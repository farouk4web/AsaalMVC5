using Asaal.Models;
using Asaal.Resources;
using Asaal.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Xml;

namespace Asaal.Controllers
{
    [Authorize(Roles = RoleName.AdminsAndSupervisors)]
    public class ControlPanelController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();


        // statistics of site
        public ActionResult Index()
        {
            var viewModel = new ControlPanelHomeVM
            {
                NumberOfAdmins = _context.Roles.SingleOrDefault(m=>m.Name ==RoleName.Admins).Users.Count(),
                NumberOfSupervisiors = _context.Roles.SingleOrDefault(m=>m.Name ==RoleName.Supervisors).Users.Count(),
                NumberOfUsers = _context.Users.Count(),

                NumberOfSocieties = _context.Societies.Count(),
                NumberOfQuestions = _context.Questions.Count(),
                NumberOfNotifications = _context.Notifications.Count(),

                NumberOfAnswers = _context.Answers.Count(),
                NumberOfReplays = _context.Replays.Count(),
                NumberOfAnswersWithReplays = _context.Answers.Where(c => c.Replays.Count != 0).Count()
            };

            return View(viewModel);
        }

        public ActionResult Admins()
        {
            var adminsRole = _context.Roles.SingleOrDefault(a => a.Name == RoleName.Admins);

            var adminsUsers = new List<ApplicationUser>();

            foreach(var role in adminsRole.Users)
            {
                var user = _context.Users.Find(role.UserId);

                adminsUsers.Add(user);
            }

            return View(adminsUsers);
        }

        public ActionResult Supervisors()
        {
            var supervisorsRole = _context.Roles.SingleOrDefault(a => a.Name == RoleName.Supervisors);

            var supervisorsUsers = new List<ApplicationUser>();

            foreach (var role in supervisorsRole.Users)
            {
                var user = _context.Users.Find(role.UserId);

                supervisorsUsers.Add(user);
            }

            return View(supervisorsUsers);
        }

        public ActionResult Users()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        public ActionResult UserAccount(string id)
        {
            var userInDb = _context.Users.Find(id);
            if (userInDb == null)
                return HttpNotFound();

            var _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));

            var viewModel = new UserAccountVM
            {
                IsAdmin = _userManager.IsInRole(id, RoleName.Admins),
                IsSupervisor = _userManager.IsInRole(id, RoleName.Supervisors),
                User = userInDb
            };

            return View(viewModel);
        }

        // create new Society and update it
        public ActionResult NewSociety()
        {
            return View("SocietyForm", new Society());
        }

        public ActionResult UpdateSociety(int id)
        {
            var societyInDb = _context.Societies.Find(id);
            if (societyInDb == null)
                return HttpNotFound();

            return View("SocietyForm", societyInDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveSociety(Society society)
        {
            if (!ModelState.IsValid)
            {
                return View("SocietyForm", society);
            }

            if (society.Id == 0)
            {
                //New society
                society.UserId = User.Identity.GetUserId();
                _context.Societies.Add(society);
            }
            else
            {
                //ancient society
                var societyInDb = _context.Societies.Find(society.Id);
                if (societyInDb == null)
                    return HttpNotFound();

                societyInDb.Name = society.Name;
                societyInDb.Description = society.Description;
            }

            _context.SaveChanges();

            return RedirectToAction("SocietyInDetails", "Societies", new { id = society.Id});
        }




        // next actios are just to Admins  *********************************************************** FOR ADMINS ***********************************************
        [Authorize(Roles = RoleName.Admins)]
        public ActionResult ChangeDefaultLanguage()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Admins)]
        public ActionResult ChangeDefaultLanguage(string language)
        {
            if (language != null)
            {
                // change the language
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

                // update langauge in web config
                Configuration configFlie = WebConfigurationManager.OpenWebConfiguration("~");
                AppSettingsSection objAppsettings = (AppSettingsSection)configFlie.GetSection("appSettings");
                if (objAppsettings != null)
                {
                    objAppsettings.Settings["defaultLanguage"].Value = language;
                    configFlie.Save();
                }
            }

            return RedirectToAction("ChangeDefaultLanguage");
        }


        [Authorize(Roles = RoleName.Admins)]
        public ActionResult ChangeFaviconAndLogo()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Admins)]
        public ActionResult ChangeFavicon(HttpPostedFileBase favicon)
        {

            if (favicon != null && favicon.ContentLength > 0)
            {
                //save new favicon
                var path = Path.Combine(Server.MapPath("~/"), "favicon.ico");
                favicon.SaveAs(path);
            }
            return RedirectToAction("index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Admins)]
        public ActionResult ChangeLogo(HttpPostedFileBase logo)
        {
            if (logo != null && logo.ContentLength > 0)
            {
                //save new logo
                var path = Path.Combine(Server.MapPath("~/Content/Images/"), "logo.png");
                logo.SaveAs(path);
            }
            return RedirectToAction("index");
        }


        // update site setting like email and password
        [Authorize(Roles = RoleName.Admins)]
        public ActionResult UpdateSiteSetting(bool? emailState, bool? sendMsgsState, bool? domainState)
        {
            var viewModel = new SiteSettingVM
            {
                SiteEmail = new SiteEmail
                {
                    Email = ConfigurationManager.AppSettings["siteEmail"].ToString(),
                    Password = ConfigurationManager.AppSettings["siteEmailPassword"].ToString()
                },
                SiteDomain = new SiteDomain
                {
                    SiteUrl = ConfigurationManager.AppSettings["siteDomain"].ToString()
                },
                SendEmailOrNot = bool.Parse(ConfigurationManager.AppSettings["sendEmailOrNot"].ToString())
            };

            ViewBag.siteEmailIsDone = emailState;
            ViewBag.SendEmailOrNotDone = sendMsgsState;
            ViewBag.siteDomainDone = domainState;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Admins)]
        public ActionResult UpdateSiteEmail(SiteSettingVM vM)
        {
            if (ModelState.IsValid)
            {
                // update email and password in web config file
                Configuration configFlie = WebConfigurationManager.OpenWebConfiguration("~");
                AppSettingsSection objAppsettings = (AppSettingsSection)configFlie.GetSection("appSettings");
                if (objAppsettings != null)
                {
                    objAppsettings.Settings["siteEmail"].Value = vM.SiteEmail.Email;
                    objAppsettings.Settings["siteEmailPassword"].Value = vM.SiteEmail.Password;
                    configFlie.Save();
                }
            }

            return RedirectToAction("UpdateSiteSetting", new { emailState = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Admins)]
        public ActionResult UpdateSiteDomain(SiteSettingVM vM)
        {
            if (ModelState.IsValid)
            {
                // update email and password in web config file
                Configuration configFlie = WebConfigurationManager.OpenWebConfiguration("~");
                AppSettingsSection objAppsettings = (AppSettingsSection)configFlie.GetSection("appSettings");
                if (objAppsettings != null)
                {
                    objAppsettings.Settings["siteDomain"].Value = vM.SiteDomain.SiteUrl;
                    configFlie.Save();
                }
            }

            return RedirectToAction("UpdateSiteSetting", new { domainState = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Admins)]
        public ActionResult EnableMesages(SiteSettingVM vM)
        {
            if (ModelState.IsValid)
            {
                // update email and password in web config file
                Configuration configFlie = WebConfigurationManager.OpenWebConfiguration("~");
                AppSettingsSection objAppsettings = (AppSettingsSection)configFlie.GetSection("appSettings");
                if (objAppsettings != null)
                {
                    objAppsettings.Settings["sendEmailOrNot"].Value = vM.SendEmailOrNot.ToString();
                    configFlie.Save();
                }
            }

            return RedirectToAction("UpdateSiteSetting", new { sendMsgsState = true });
        }



        //create roles and add users to role and remove thim
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Admins)]
        public ActionResult AddUserToRole(string userId, string roleName, bool inPanel)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(roleName))
                return RedirectToAction("UserProfile", "Account", new { id = userId });

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
            UserManager.AddToRole(userId, roleName);

            if (inPanel == true)
                return RedirectToAction("UserAccount", "ControlPanel", new { id = userId });
            else
                return RedirectToAction("UserProfile", "Account", new { id = userId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Admins)]
        public ActionResult RemoveUserFromRole(string userId, string roleName, bool inPanel)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(roleName) || userId == "faa86e49-7b77-426b-9021-a1602d967184")
                return RedirectToAction("UserProfile", "Account", new { id = userId });

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));
            UserManager.RemoveFromRole(userId, roleName);

            if (inPanel == true)
                return RedirectToAction("UserAccount", "ControlPanel", new { id = userId });
            else
                return RedirectToAction("UserProfile", "Account", new { id = userId });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Admins)]
        public ActionResult RemoveAccount(string userId, bool inPanel)
        {
            var userInDb = _context.Users.Find(userId);

            if (userInDb != null && userId != "faa86e49-7b77-426b-9021-a1602d967184")
            {
                _context.Users.Remove(userInDb);
                _context.SaveChanges();
            }

            if (inPanel == true)
                return RedirectToAction("Users", "ControlPanel");
            else
                return RedirectToAction("index", "questions");
        }

    }
}