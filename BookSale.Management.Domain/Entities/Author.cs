using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookSale.Management.Domain.Entities
{
    
    public class Author : BaseEntity
    {
        [Required]
        [StringLength(20)]
        public string Code { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        [StringLength(int.MaxValue)]
        public string Biography { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(1000)]
        public string Address { get; set; }

        [StringLength(7)]
        public string CreatedBy { get; set; }

        [Required]
        public bool IsActive { get; set; }

        // Navigation properties và các relationship có thể được thêm vào đây
        public ICollection<Book> Books { get; set; }
    }
}