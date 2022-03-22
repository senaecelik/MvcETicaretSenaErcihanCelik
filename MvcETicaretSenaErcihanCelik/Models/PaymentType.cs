using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcETicaretSenaErcihanCelik.Models
{
    public class PaymentType
    {
        [Key]
        public int PaymentTypeID { get; set; }
        [Required, MaxLength(100)]
        public string TypeName { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }

       
}