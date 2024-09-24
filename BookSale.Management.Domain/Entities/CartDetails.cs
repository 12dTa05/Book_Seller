using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Management.Domain.Entities
{
    public class CartDetails : BaseEntity
    {
        public int CartId { get; set; }

        public int BookId { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string? Note { get; set; }
        [ForeignKey(nameof(CartId))]
        public Cart Cart { get; set; }
        [ForeignKey(nameof(BookId))]
        public Book Book { get; set; }

    }
}
