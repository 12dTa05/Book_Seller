using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Management.Application.DTOs
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string? Fullname { get; set; } // Cho phép null
        public string? Email { get; set; }    // Cho phép null
        public string? Phone { get; set; }    // Cho phép null
        public bool IsActive { get; set; }
    }



}
