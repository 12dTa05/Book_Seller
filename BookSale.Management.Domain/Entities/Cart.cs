using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Management.Domain.Entities
{
    public class Cart : BaseEntity
    {
        [Required]
        public string UserId { get; set; }
        public string Code { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        [StringLength(1000)]
        public string? Note { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
