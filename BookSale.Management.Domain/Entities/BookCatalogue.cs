using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Management.Domain.Entities
{
    public class BookCatalogue : BaseEntity
    {
        public DateTime CreatedOn { get; set; }
        public int BookId { get; set; }
        public int CatalogueId { get; set; }
        [ForeignKey(nameof(BookId))]
        public Book Book { get; set; }
        [ForeignKey(nameof(CatalogueId))]
        public Catalogue Catalogue { get; set; }
    }
}
