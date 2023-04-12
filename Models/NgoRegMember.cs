using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace NgoProjectNew1.Models
{
    public partial class NgoRegMember
    {
        public int MemberId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }

        [Required(ErrorMessage = "Mobile is required")]
        [RegularExpression(@"\d{10}", ErrorMessage = "Please enter 10 digit Mobile No.")]
        public string ContactNo { get; set; }
        public int? RoleId { get; set; }
        public string IsActive { get; set; }
        public string AdminComments { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string ModifiedBy { get; set; }
        [Required]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
   
        public virtual NgoUserRole Role { get; set; }
    }
}
