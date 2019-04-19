using AutoMapper;
using BloodDonation.BloodDonationEntities;
using BloodDonation.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace BloodDonation.Controllers
{
    public class AccountController : Controller
    {
        private readonly TestBloodEntities context;

        public AccountController()
        {
            context = new TestBloodEntities();
        }

        #region Register
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserViewModel model)
        {
            User dbUser = Mapper.Map<UserViewModel, User>(model);

            try
            {
                context.Users.Add(dbUser);
                context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Register", "Account");
            }
        }
        #endregion

        #region Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            try
            {
                if(context.Users.Where(u => u.Email == model.Email && u.Password == model.Password).Count() > 0)
                {
                    Session["logged_in"] = "true";

                    // If you need role base authentication => 
                    // Session["Role"] = .....
                    // Access this role session variable in html view and hide/display content as per your requirement.
                    // for reference see Index.cshtml file in Views/Home folder

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Error", "Invalid Cradiation");
                    return View();
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("Error", "Invalid Cradiation");
                return View(model);
            }
        }
        #endregion

        #region Logout
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }
        #endregion
    }
}