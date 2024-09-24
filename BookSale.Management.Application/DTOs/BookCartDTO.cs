using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSale.Management.Application.DTOs
{
    public class BookCartDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public double Price { get; set; }
        public string Title { get; set; }
        public int Quantity { get; set; }
        public double Total { get; set; }
    }
}