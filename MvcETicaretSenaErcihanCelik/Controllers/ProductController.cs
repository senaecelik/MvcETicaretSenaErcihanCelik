using MvcETicaretSenaErcihanCelik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcETicaretSenaErcihanCelik.Controllers
{
    public class ProductController : Controller
    {
        ApplicationDbContext db;
        public ProductController()
        {
            db = new ApplicationDbContext();
        }
        public ActionResult Product(int id)
        {
            return View(db.Products.Where(x=>x.SubCategoryID==id).ToList());
        }

        public ActionResult ProductDetail(int id)
        {
            TempData["Reviews"]= db.Reviews.Where(x => x.ProductID == id).ToList();
            return View(db.Products.Where(x => x.ProductID == id).FirstOrDefault());
        }

        public ActionResult AddReview(int id, FormCollection frm)
        {
            Review review = new Review();

            review.CustomerID = TemporaryUserData.UserID;
            review.ProductID = id;
            review.Name = frm["name"] == "" ? "Anonymous" : frm["name"];
            review.Review1 = frm["review"];
            review.Rate = int.Parse(frm["rating"]);
            review.DateTime = DateTime.Now;
            review.IsDeleted = false;

            db.Reviews.Add(review);
            db.SaveChanges();

            return RedirectToAction("ProductDetail", new { id = review.ProductID });
        }
    }
}