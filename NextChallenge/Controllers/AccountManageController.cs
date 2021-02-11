using NextChallenge.Database.Repository;
using NextChallenge.Models;
using System;
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
            Session["IdUser"] = Guid.Empty;

            return View();
        }
        public ActionResult CreateRegister()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            Reset();
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
                default:
                    var userInfo = _userRepository.UserInfo(new UserInfoInput { Username = input.Username });
                    Session["IdUser"] = userInfo.IdUser;
                    Session["Username"] = userInfo.Username;
                    return RedirectToAction("Index", "Topic");
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
                default:
                    var userInfo = _userRepository.UserInfo(new UserInfoInput { Username = input.Username });
                    Session["IdUser"] = userInfo.IdUser;
                    Session["Username"] = userInfo.Username;
                    return RedirectToAction("Index", "Topic");

            }
        }
        public HttpStatusCode Reset()
        {
            Session["IdUser"] = Guid.Empty;
            Session["Username"] = string.Empty;
            return HttpStatusCode.OK;
        }
    }
}