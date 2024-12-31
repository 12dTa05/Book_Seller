using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Management.Application.DTOs.ViewsModel
{
    public class GenreViewModel
    {
        public int? Id { get; set; } = 0;
        [Required(ErrorMessage = "Genre name must not be empty")]
        public string Name { get; set; }
    }
}
