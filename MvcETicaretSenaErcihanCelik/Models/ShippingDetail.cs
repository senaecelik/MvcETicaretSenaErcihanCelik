using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcETicaretSenaErcihanCelik.Models
{
    public class ShippingDetail
    {
        [Key]
        public int ShippingID { get; set; }
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }
        [Required,MaxLength(50)]
        public string Email { get; set; }
        [Required, MaxLength(50)]
        public string Mobile { get; set; }
        [Required, MaxLength(50)]
        public string Address { get; set; }
        [Required,MaxLength(50)]
        public string City { get; set; }
        [Required,MaxLength(50)]
        public string PostalCode { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}