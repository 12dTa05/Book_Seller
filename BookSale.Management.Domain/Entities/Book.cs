using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Management.Domain.Entities
{
    public class Book : BaseEntity
    {
        [StringLength(20)]
        public string Code { get; set; }
        [Required]
        [StringLength(500)]
        public string? Title { get; set; }
        [Required]
        public int Available { get; set; }
        [StringLength(500)]
        public string? Publisher { get; set; }
        public double Cost { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        [Required]
        [StringLength(250)]
        public string Description { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public int GenreId { get; set; }
        public int AuthorId { get; set; }
        [ForeignKey(nameof(GenreId))]
        public Genre Genre { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public Author Author { get; set; }
    }
}
