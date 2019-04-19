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
            List<Blood> bloods = context.Bloods.ToList();
            List<BloodViewModel> model = Mapper.Map<List<Blood>, List<BloodViewModel>>(bloods);

            return View(model);
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

                    return RedirectToAction("AllDonor", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Either email address or password is incorrect.");
                    model.Blood = context.Bloods.ToList();
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
        public ActionResult AllDonor()
        {
            List<Donor> donors = context.Donors.ToList();
            List<DonorViewModel> model = new List<DonorViewModel>();

            foreach (Donor d in donors)
            {
                DonorViewModel donorListViewModel = new DonorViewModel()
                {
                    DonorId = d.DonorId,
                    User = Mapper.Map<User, UserViewModel>(d.User),
                    Blood = Mapper.Map<Blood, BloodViewModel>(d.Blood)
                };
                model.Add(donorListViewModel);
            }

            return View(model);
        }

        public ActionResult DonorList(int id)
        {
            List<Donor> donors = context.Donors
                                    .Where(d => d.BloodId == id)
                                    .ToList();

            List<DonorViewModel> model = new List<DonorViewModel>();

            foreach (Donor d in donors)
            {
                DonorViewModel donorListViewModel = new DonorViewModel()
                {
                    DonorId = d.DonorId,
                    User = Mapper.Map<User, UserViewModel>(d.User),
                    Blood = Mapper.Map<Blood, BloodViewModel>(d.Blood)
                };
                model.Add(donorListViewModel);
            }

            return View(model);
        }
        #endregion

        #region Donor Details
        public ActionResult DonorDetails(int id)
        {
            Donor donor = context.Donors
                .Where(d => d.DonorId == id)
                .FirstOrDefault();

            DonorViewModel model = new DonorViewModel()
            {
                DonorId = donor.DonorId,
                Blood = Mapper.Map<Blood, BloodViewModel>(donor.Blood),
                User = Mapper.Map<User, UserViewModel>(donor.User)
            };

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