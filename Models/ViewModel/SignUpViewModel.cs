
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NgoProjectNew1.Models.ViewModel
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "UserName is Required")]
        [Remote(action: "UserNameIsExist", controller:"Home")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }
        public string Address { get; set; }

        [Required(ErrorMessage = "Mobile is required")]
        [RegularExpression(@"\d{10}", ErrorMessage = "Please enter 10 digit Mobile No.")]
        public string ContactNo { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
