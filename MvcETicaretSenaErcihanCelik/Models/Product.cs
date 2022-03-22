using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcETicaretSenaErcihanCelik.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        [MaxLength(150), Required]
        public string ProductName { get; set; }
        [ForeignKey("Supplier"), Required]
        public int SupplierID { get; set; }
        [ForeignKey("SubCategory")]
        public int SubCategoryID { get; set; }
        [MaxLength(20)]
        public string QuantityPerUnit { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        public decimal OldPrice { get; set; }
        [MaxLength(50)]
        public string UnitWeight { get; set; }
        [MaxLength(50)]
        public string Size { get; set; }
        [Required]
        public decimal Discount { get; set; }
        [Required]
        public int UnitInStock { get; set; }
        public int UnitOnOrder { get; set; }
        public bool ProdutAvailable { get; set; }
        [MaxLength(500)]
        public string ImageUrl { get; set; }
        [MaxLength(150)]
        public string AltText { get; set; }
        [MaxLength(300), Required]
        public string ShortDescription { get; set; }
        [MaxLength(3000),Required]
        public string LongDescription { get; set; }
        [MaxLength(500)]
        public string Picture1 { get; set; }
        [MaxLength(500)]
        public string Picture2 { get; set; }
        [MaxLength(500)]
        public string Picture3 { get; set; }
        [MaxLength(500)]
        public string Picture4 { get; set; }
        [MaxLength(250)]
        public string Notes { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<Wishlist> Wishlists { get; set; }
        public virtual ICollection<RecentlyView> RecentlyViews { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }


    }
}