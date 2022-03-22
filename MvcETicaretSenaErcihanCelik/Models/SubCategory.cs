using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcETicaretSenaErcihanCelik.Models
{
    public class SubCategory
    {
        [Key]
        public int SubCategoryID { get; set; }
        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        [MaxLength(100), Required]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        [MaxLength(500)]
        public string Picture1 { get; set; }
        [MaxLength(500)]
        public string Picture2 { get; set; }
        public bool IsActive { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Product> Products { get; set; }

    }
}