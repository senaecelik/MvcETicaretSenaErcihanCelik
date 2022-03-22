using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcETicaretSenaErcihanCelik.Models
{
    public class AdminLogin
    {
        [Key]
        public int LoginID { get; set; }
        [Required,ForeignKey("AdminEmployee")]
        public int EmpID { get; set; }
        [Required, MaxLength(50)]
        public string UserName { get; set; }
        [Required, MaxLength(50)]
        public string Password { get; set; }
        [Required, ForeignKey("Role")]
        public int RoleType { get; set; }

        public virtual AdminEmployee AdminEmployee { get; set; }
        public virtual Role Role { get; set; }


    }
}