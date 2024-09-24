using BookSale.Management.Application.Abstracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookSale.Management.Application.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<bool> SaveImage(List<IFormFile> images, string path, string? defaultName)
        {
            if (images == null || images.Count == 0 || string.IsNullOrEmpty(path))
            {
                return false;
            }

            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path); // e.g., wwwroot/img/user

            try
            {
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                foreach (var image in images)
                {
                    if (image == null || image.Length == 0)
                    {
                        continue;
                    }

                    // Kiểm tra định dạng file
                    var permittedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                    var extension = Path.GetExtension(image.FileName).ToLowerInvariant();

                    if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
                    {
                        // Tệp không hợp lệ, bỏ qua
                        continue;
                    }

                    // Tạo tên file duy nhất để tránh trùng lặp
                    string uniqueFileName;
                    if (!string.IsNullOrEmpty(defaultName))
                    {
                        // Nếu có tên mặc định, sử dụng nó kèm theo phần mở rộng
                        uniqueFileName = $"{defaultName}{extension}";
                    }
                    else
                    {
                        // Nếu không, tạo tên file duy nhất bằng cách thêm GUID
                        uniqueFileName = $"{Path.GetFileNameWithoutExtension(image.FileName)}_{Guid.NewGuid()}{extension}";
                    }

                    // Đảm bảo tên file không chứa các ký tự không hợp lệ
                    uniqueFileName = Path.GetFileName(uniqueFileName);

                    // Tạo đường dẫn đầy đủ đến file
                    string filePath = Path.Combine(uploadPath, uniqueFileName);

                    // Lưu tệp vào thư mục
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }
                }

                return true;
            }
            catch
            {
                // Nếu xảy ra lỗi, trả về false
                return false;
            }
        }
    }
}
