using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Management.Application.DTOs
{
    public class AuthorDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Author code is required")]
        [StringLength(20, ErrorMessage = "Author code cannot exceed 20 characters")]
        [DisplayName("Author Code")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Author name is required")]
        [StringLength(500, ErrorMessage = "Author name cannot exceed 500 characters")]
        [DisplayName("Author Name")]
        public string Name { get; set; }

        [DisplayName("Biography")]
        public string Biography { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        [DisplayName("Email Address")]
        public string Email { get; set; }

        [StringLength(1000, ErrorMessage = "Address cannot exceed 1000 characters")]
        [DisplayName("Address")]
        public string Address { get; set; }

        [StringLength(7, ErrorMessage = "CreatedBy cannot exceed 7 characters")]
        [DisplayName("Created By")]
        public DateTime CreatedOn { get; set; }

        
    }
}
