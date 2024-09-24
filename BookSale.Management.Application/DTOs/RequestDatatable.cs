using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BookSale.Management.Application.DTOs
{
    public class RequestDatatable
    {
        [BindProperty(Name = "length")]
        public int PageSize { get; set; }

        [BindProperty(Name = "start")]
        [Range(0, int.MaxValue, ErrorMessage = "Start must be a non-negative integer.")]
        public int SkipItems { get; set; }

        [BindProperty(Name = "search[value]")]
        public string? Keyword { get; set; }

        [BindProperty(Name = "draw")]
        public int Draw { get; set; }
    }
}
