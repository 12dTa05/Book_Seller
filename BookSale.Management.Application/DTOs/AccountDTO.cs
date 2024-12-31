using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Management.Application.DTOs
{
    public class AccountDTO 
    {
        public string? Id { get; set; }
        [Required]
        public string RoleName { get; set; }
        [Required(ErrorMessage = "Username must be not empty")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Fullname must be not empty")]
        public string Fullname { get; set; }
        [Required(ErrorMessage = "Password must be not empty")]
        public string Password { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; }
        public IFormFile? Avatar { get; set; }

    }
}
