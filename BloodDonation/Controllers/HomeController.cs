using AutoMapper;
using BloodDonation.BloodDonationEntities;
using BloodDonation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BloodDonation.Controllers
{
    public class HomeController : Controller
    {
        private readonly TestBloodEntities context;

        public HomeController()
        {
            context = new TestBloodEntities();
        }

        public ActionResult Index()
        {
            return View();
        }

        #region Donate Form
        [HttpGet]
        public ActionResult Donate()
        {
            DonateViewModel model = new DonateViewModel
            {
                Blood = context.Bloods.ToList()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Donate(DonateViewModel model)
        {
            try
            {
                if (context.Users.Where(u => u.Email == model.User.Email && u.Password == model.User.Password).Count() > 0)
                {
                    User user = context.Users
                                    .Where(u => u.Email == model.User.Email && u.Password == model.User.Password)
                                    .FirstOrDefault();

                    Donor donor = new Donor()
                    {
                        BloodId = model.BloodId,
                        UserId = user.UserId
                    };

                    context.Donors.Add(donor);
                    context.SaveChanges();

                    return RedirectToAction("DonorList", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Either email address or password is incorrect.");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Donate", "Home");
            }
        }
        #endregion

        #region Donors List
        public ActionResult DonorList()
        {
            List<Donor> donors = context.Donors.ToList();
            List<DonorListViewModel> model = new List<DonorListViewModel>();

            foreach(Donor d in donors)
            {
                DonorListViewModel donorListViewModel = new DonorListViewModel()
                {
                    User = Mapper.Map<User, UserViewModel>(d.User),
                    Blood = Mapper.Map<Blood, BloodViewModel>(d.Blood)
                };
                model.Add(donorListViewModel);
            }

            return View(model);
        }
        #endregion
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}