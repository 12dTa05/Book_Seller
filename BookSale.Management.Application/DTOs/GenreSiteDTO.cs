using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSale.Management.Application.DTOs
{
    public class GenreSiteDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int TotalBooks { get; set; }
    }
}