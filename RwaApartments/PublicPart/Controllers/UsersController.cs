using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PublicPart.Models.Auth;
using PublicPart.Models.CustomAttributes;
using PublicPart.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PublicPart.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private UserManager _authManager;
        private SignInManager _signInManager;

        public SignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<SignInManager>();
            }
            set
            {
                _signInManager = value;
            }
        }

        public UserManager AuthManager
        {
            get
            {
                return _authManager ?? HttpContext.GetOwinContext().Get<UserManager>();
            }
            set
            {
                _authManager = value;
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [IsAuthorized]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await AuthManager.FindAsync(model.Email, model.Password);

            if (user != null)
            {
                await SignInManager.SignInAsync(user, true, model.RememberMe);
                return RedirectToAction("Index", "Apartments");
            }
            else
            {
                ModelState.AddModelError("", "Korisnik ne postoji u bazi");
                return View(model);
            }


        }
        [HttpGet]
        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction(actionName: "Index", controllerName: "Apartments");
        }
    }
}