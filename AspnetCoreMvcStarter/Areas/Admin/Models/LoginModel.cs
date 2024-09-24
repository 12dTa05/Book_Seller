﻿using System.ComponentModel.DataAnnotations;

namespace BookSale.Management.UI.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username must be not empty")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password must be not empty")]
        [MinLength(3, ErrorMessage = "Password must be at least 3 characters long")]
        public string Password { get; set; }

        public bool HasRemember { get; set; }
    }
}
