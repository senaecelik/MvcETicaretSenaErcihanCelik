using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcETicaretSenaErcihanCelik.Models
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }
        [Required, MaxLength(50)]
        public string RoleName { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public virtual ICollection<AdminLogin> AdminLogins { get; set; }


    }
}