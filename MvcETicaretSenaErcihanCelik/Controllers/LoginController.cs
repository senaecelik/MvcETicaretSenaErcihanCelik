using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcETicaretSenaErcihanCelik.Models;

namespace MvcETicaretSenaErcihanCelik.Controllers
{
    public class LoginController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection frm)
        {
            string kullaniciAdi = frm["username"];
            string sifre = frm["password"];

            Customer customer = db.Customers.Where(x => x.UserName == kullaniciAdi && x.Password == sifre).FirstOrDefault();

            if (customer != null)
            {
                Session["kullanici"] = customer.UserName;
                TemporaryUserData.UserID = customer.CustomerID;

                customer.LastLogin = DateTime.Now;
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public ActionResult Logout()
        {

            TemporaryUserData.UserID = 0;
            Session["kullanici"] = null;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(FormCollection frm)
        {
            Customer customer = new Customer();

            customer.FirstName = frm["name"];
            customer.LastName = frm["surname"];
            customer.UserName = frm["username"];
            customer.Password = frm["password"];
            customer.Age = Convert.ToInt32(frm["age"]);
            customer.Gender = frm["gender1"] != null ? true : false;
            customer.BirthDate = Convert.ToDateTime(frm["birthdate"]).Date;
            customer.LastLogin = DateTime.Now;
            customer.Created = DateTime.Now.Date;

            db.Customers.Add(customer);
            db.SaveChanges();

            Session["kullanici"] = customer.UserName;
            TemporaryUserData.UserID = customer.CustomerID;

            return RedirectToAction("Index", "Home");
        }
    }
}