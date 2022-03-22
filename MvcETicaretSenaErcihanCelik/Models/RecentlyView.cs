using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcETicaretSenaErcihanCelik.Models
{
    public class RecentlyView
    {
        [Key]
        public int RViewID { get; set; }
        [ForeignKey("Customer"),Required]
        public int CustomerID { get; set; }

        [ForeignKey("Product"), Required]
        public int ProductID { get; set; }
        [Required]
        public DateTime ViewDate { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
        
    }
}