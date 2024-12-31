using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookSale.Management.Application.Abstracts
{
    public interface IImageService
    {
        Task<bool> SaveImage(List<IFormFile> images, string path, string? defaultName);
    }
}
