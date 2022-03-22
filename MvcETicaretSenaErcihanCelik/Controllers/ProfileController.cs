using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcETicaretSenaErcihanCelik.Models;

namespace MvcETicaretSenaErcihanCelik.Controllers
{
    public class ProfileController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult UpdateProfile()
        {
            return View(db.Customers.Find(TemporaryUserData.UserID));
        }

        [HttpPost]
        public ActionResult UpdateProfile(FormCollection frm)
        {
            Customer customer = db.Customers.Find(TemporaryUserData.UserID);

            customer.FirstName = frm["FirstName"];
            customer.LastName = frm["LastName"];
            customer.Password = frm["Password"];
            customer.Age = Convert.ToInt32(frm["Age"]);
            customer.Gender = frm["Gender"] == "on" ? true: false;
            customer.BirthDate = DateTime.Parse(frm["BirthDate"]);
            customer.Email = frm["Email"];
            customer.Mobile1 = frm["Mobile1"];
            customer.Address1 = frm["Address1"];
            customer.City = frm["City"];
            customer.PostalCode = frm["PostalCode"];

            db.SaveChanges();

            return RedirectToAction("UpdateCompleted");
        }
        public ActionResult UpdateCompleted()
        {
            return View();
        }
    }
}