using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcETicaretSenaErcihanCelik.Models;
using System.Web.Mvc;

namespace MvcETicaretSenaErcihanCelik.Controllers
{
    public class ShoppingController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult AddToCart(int id)
        {
            OrderDetail od = db.OrderDetails.Where(x => x.ProductID == id && x.CustomerID == TemporaryUserData.UserID && x.IsCompleted==false).FirstOrDefault();

            if (od == null)
            {
                od = new OrderDetail();

                od.ProductID = id;
                od.CustomerID = TemporaryUserData.UserID;
                od.IsCompleted = false;
                od.UnitPrice = db.Products.Find(id).UnitPrice;
                od.Quantity = 1;
                od.Discount = db.Products.Find(id).Discount;
                od.TotalAmount = od.UnitPrice * od.Quantity * (1 - od.Discount);
                od.OrderDate = DateTime.Now;

                db.OrderDetails.Add(od);
            }
            else
            {
                od.Quantity++;
                od.TotalAmount = od.UnitPrice * od.Quantity * (1 - od.Discount);
            }
            db.SaveChanges();

            return RedirectToAction("ProductDetail", "Product", new { id = od.ProductID });
        }
        public ActionResult AddToWishList(int id)
        {
            Wishlist wishlist = db.Wishlists.Where(x => x.ProductID == id && x.CustomerID == TemporaryUserData.UserID).FirstOrDefault();

            if (wishlist == null)
            {
                wishlist = new Wishlist();

                wishlist.CustomerID = TemporaryUserData.UserID;
                wishlist.ProductID = id;
                wishlist.IsActive = true;

                db.Wishlists.Add(wishlist);
                db.SaveChanges();
            }

            return RedirectToAction("ProductDetail", "Product", new { id = wishlist.ProductID });
        }

        public ActionResult Cart()
        {
            return View(db.OrderDetails.Where(x => x.CustomerID == TemporaryUserData.UserID && x.IsCompleted == false).ToList());
        }
        public ActionResult Wishlist()
        {
            return View(db.Wishlists.Where(x => x.CustomerID == TemporaryUserData.UserID && x.IsActive == true).ToList());
        }

        public ActionResult RemoveFromCart(int id)
        {
            OrderDetail od = db.OrderDetails.FirstOrDefault(x => x.ProductID == id && x.CustomerID == TemporaryUserData.UserID);
            db.OrderDetails.Remove(od);
            db.SaveChanges();
            return RedirectToAction("Cart");
        }
        public ActionResult AddToWishlistFromCart(int id)
        {
            OrderDetail od = db.OrderDetails.FirstOrDefault(x => x.ProductID == id && x.CustomerID == TemporaryUserData.UserID);

            db.OrderDetails.Remove(od);
            Wishlist wishlist = db.Wishlists.FirstOrDefault(x => x.ProductID == id && x.CustomerID == TemporaryUserData.UserID);


            if (wishlist == null)
            {
                wishlist = new Wishlist();
                wishlist.CustomerID = TemporaryUserData.UserID;
                wishlist.ProductID = id;
                wishlist.IsActive = true;

                db.Wishlists.Add(wishlist);
            }
            db.SaveChanges();
            return RedirectToAction("Cart");
        }
        public ActionResult RemoveToWishlist(int id)
        {
            Wishlist wishlist = db.Wishlists.FirstOrDefault(x => x.ProductID == id && x.CustomerID == TemporaryUserData.UserID);
            db.Wishlists.Remove(wishlist);

            db.SaveChanges();
            return RedirectToAction("Wishlist");
        }

        
        public ActionResult AddToCartFromWishlist(int id)
        {

            Wishlist wishlist = db.Wishlists.FirstOrDefault(x => x.ProductID == id && x.CustomerID == TemporaryUserData.UserID);
            db.Wishlists.Remove(wishlist);

            OrderDetail od = db.OrderDetails.Where(x => x.ProductID == id && x.CustomerID == TemporaryUserData.UserID).FirstOrDefault();

            if (od == null)
            {
                od = new OrderDetail();

                od.ProductID = id;
                od.CustomerID = TemporaryUserData.UserID;
                od.IsCompleted = false;
                od.UnitPrice = db.Products.Find(id).UnitPrice;
                od.Quantity = 1;
                od.Discount = db.Products.Find(id).Discount;
                od.TotalAmount = od.UnitPrice * od.Quantity * (1 - od.Discount);
                od.OrderDate = DateTime.Now;

                db.OrderDetails.Add(od);
            }
            else
            {
                od.Quantity++;
                od.TotalAmount = od.UnitPrice * od.Quantity * (1 - od.Discount);
            }
            db.SaveChanges();

            return RedirectToAction("ProductDetail", "Product", new { id = od.ProductID });
        }

        public ActionResult UpdateQuantity(int id, FormCollection frm)
        {
            OrderDetail od = db.OrderDetails.Find(id);
            od.Quantity = int.Parse(frm["NewQuantity"]);

            db.SaveChanges();
            return RedirectToAction("Cart");
        }

        public ActionResult CompleteOrders()
        {
            return View(db.PaymentTypes.ToList());
        }
        [HttpPost]
        public ActionResult ContinueShopping(FormCollection frm)
        {
            Payment p = new Payment();
            p.Type = int.Parse(frm["PaymentType"]);
            p.PaymentDateTime = DateTime.Now;

            db.Payments.Add(p);
            db.SaveChanges();
            TemporaryUserData.PaymentID = p.PaymentID;
            return View(db.Customers.Find(TemporaryUserData.UserID));
        }
        [HttpPost]
        public ActionResult CompleteShopping(FormCollection frm)
        {
            ShippingDetail shippingDetail = new ShippingDetail();

            shippingDetail.FirstName = frm["FirstName"];
            shippingDetail.LastName = frm["LastName"];
            shippingDetail.Email = frm["Email"];
            shippingDetail.Mobile = frm["Mobile1"];
            shippingDetail.Address = frm["Address1"];
            shippingDetail.City = frm["City"];
            shippingDetail.PostalCode = frm["PostalCode"];

            db.ShippingDetails.Add(shippingDetail);
            db.SaveChanges();

            foreach (var item in db.OrderDetails.Where(x => x.CustomerID == TemporaryUserData.UserID && x.IsCompleted == false).ToList())
            {
                item.IsCompleted = true;
               db.Products.Find(item.ProductID).UnitInStock -= item.Quantity;

                Order order = new Order();
                order.OrderDetailID = item.OrderDetailID;
                order.PaymentID = TemporaryUserData.PaymentID;
                order.ShippingID = shippingDetail.ShippingID;
                order.Discount = item.Discount;
                order.TotalAmount = item.TotalAmount;
                order.IsCompleted = true;
                order.OrderDate = DateTime.Now;
                order.Dispatched = false;
                order.DispatchedDate = DateTime.Now.AddDays(2);
                order.Shipped = false;
                order.ShippedDate = DateTime.Now.AddDays(3);
                order.Deliver = false;
                order.DeliveryDate = DateTime.Now.AddDays(4);
                order.CancelOrder = false;

                db.Orders.Add(order);

            }

            db.SaveChanges();

            return View();
        }


    }
}