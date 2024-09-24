using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Management.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(500)]
        public string? Fullname { get; set; }
        [StringLength(1000)]
        public string? Address { get; set; }
        [Required]
        public bool IsActive { get; set; }

        
    }
}
