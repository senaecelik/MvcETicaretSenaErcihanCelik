using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcETicaretSenaErcihanCelik.Models
{
    public class AdminEmployee
    {
        [Key]
        public int AdminEmpID { get; set; }
        [Required, MaxLength(20)]
        public string FirstName { get; set; }
        [Required, MaxLength(20)]
        public string LastName { get; set; }
        public int Age { get; set; }
        [Column(TypeName ="date")]
        public DateTime BirthDate { get; set; }
        public bool Gender { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(100)]
        public string Address { get; set; }
        [MaxLength(20)]
        public string Phone { get; set; }
        [MaxLength(20)]
        public string Mobile { get; set; }
        [MaxLength(500)]
        public string PhotoPath { get; set; }
        public virtual ICollection<AdminLogin> AdminLogins { get; set; }
        

    }
}