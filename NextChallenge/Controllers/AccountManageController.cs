using NextChallenge.Database.Repository;
using NextChallenge.Helpers;
using NextChallenge.Models;
using System.Net;
using System.Web.Mvc;

namespace NextChallenge.Controllers {
    public class AccountManageController : Controller {
        private UserRepository _userRepository;

        public AccountManageController()
        {
            _userRepository = new UserRepository();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult CreateRegister()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            Security.Reset();
            return RedirectToAction("Login");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginInput input)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var checkLogin = _userRepository.Login(input);
            switch (checkLogin)
            {
                case HttpStatusCode.Unauthorized:
                    ModelState.AddModelError("LoginError", "Your Password or User is wrong. Please Try again.");
                    return View();
                default: return RedirectToAction("Index", "Topic");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRegister(RegisterInput input)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var checkRegister = _userRepository.Register(input);
            switch (checkRegister)
            {
                case HttpStatusCode.InternalServerError:
                    ModelState.AddModelError("RegisterError", "Internal Server Error");
                    return View();
                case HttpStatusCode.Ambiguous:
                    ModelState.AddModelError("RegisterError", "Already there is this username");
                    return View();
                default: return RedirectToAction("Index", "Topic");

            }
        }
    }
}