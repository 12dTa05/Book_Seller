using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BookSale.Management.Application.DTOs.ViewsModel
{
    public class BookViewModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Genre is required")]
        public int GenreId{ get; set; }
        public string Code { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Author is required")]
        public string Author { get; set; }
        [Required(ErrorMessage = "Available is required")]
        public int Available { get; set; }
        public string? Publisher { get; set; }
        [Required(ErrorMessage = "Cost is required")]
        public double Cost { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public IFormFile? Image { get; set; }
        
    }
}
