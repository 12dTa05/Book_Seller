﻿using AutoMapper;
using BookSale.Management.Application.Abstracts;
using BookSale.Management.Application.DTOs;
using BookSale.Management.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookSale.Management.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;

        public UserService(UserManager<ApplicationUser> userManager, IImageService imageService, IMapper mapper)
        {
            _userManager = userManager;
            _imageService = imageService;
            _mapper = mapper;
        }

        public async Task<ResponseDatatable<UserModel>> GetUserByPagination(RequestDatatable requestDatatable)
        {
            // Tổng số bản ghi trước khi lọc
            var totalRecords = await _userManager.Users.CountAsync();

            var query = _userManager.Users.AsQueryable();

            // Xử lý tìm kiếm
            if (!string.IsNullOrEmpty(requestDatatable.Keyword))
            {
                if (bool.TryParse(requestDatatable.Keyword, out bool isActive))
                {
                    query = query.Where(x => x.IsActive == isActive);
                }
                else
                {
                    query = query.Where(x =>
                        x.UserName.Contains(requestDatatable.Keyword) ||
                        x.Email.Contains(requestDatatable.Keyword) ||
                        x.Fullname.Contains(requestDatatable.Keyword)
                    );
                }
            }

            // Tổng số bản ghi sau khi lọc
            var filteredRecords = await query.CountAsync();

            // Phân trang với OrderBy
            var results = await query
                .OrderBy(x => x.UserName) // Đảm bảo thứ tự
                .Skip(requestDatatable.SkipItems)
                .Take(requestDatatable.PageSize)
                .Select(x => new UserModel
                {
                    Email = x.Email,
                    Fullname = x.Fullname,
                    Username = x.UserName,
                    Phone = x.PhoneNumber,
                    IsActive = x.IsActive,
                    Id = x.Id
                })
                .ToListAsync();

            return new ResponseDatatable<UserModel>
            {
                Draw = requestDatatable.Draw,
                Data = results,
                RecordsFiltered = filteredRecords,
                RecordsTotal = totalRecords
            };
        }

        public async Task<AccountDTO> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return null;
            }

            var roles = await _userManager.GetRolesAsync(user);
            var roleName = roles.FirstOrDefault() ?? string.Empty;

            var userDto = _mapper.Map<AccountDTO>(user);
            userDto.RoleName = roleName;

            return userDto;
        }

        public async Task<ResponseModel> Save(AccountDTO accountDTO)
        {
            string errors = string.Empty;
            IdentityResult identityResult;

            if (string.IsNullOrEmpty(accountDTO.Id))
            {
                // Tạo mới người dùng
                var applicationUser = new ApplicationUser
                {
                    Fullname = accountDTO.Fullname,
                    UserName = accountDTO.Username,
                    IsActive = accountDTO.IsActive,
                    Email = accountDTO.Email,
                    PhoneNumber = accountDTO.Phone,
                    Address = accountDTO.Address
                };

                identityResult = await _userManager.CreateAsync(applicationUser, accountDTO.Password);

                if (identityResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(applicationUser, accountDTO.RoleName);

                    if (accountDTO.Avatar != null)
                    {
                        // Kiểm tra định dạng file
                        var extension = Path.GetExtension(accountDTO.Avatar.FileName).ToLowerInvariant();
                        if (!new[] { ".jpg", ".jpeg", ".png" }.Contains(extension))
                        {
                            return new ResponseModel
                            {
                                Action = Domain.Enums.ActionType.Update,
                                Message = "Chỉ chấp nhận định dạng JPG, JPEG hoặc PNG.",
                                Status = false
                            };
                        }

                        // Đặt tên file: Sử dụng ID của người dùng
                        var fileName = $"{accountDTO.Id}{extension}";

                        // Đóng gói Avatar vào danh sách
                        var fileList = new List<IFormFile> { accountDTO.Avatar };

                        // Gọi SaveImage
                        bool saveResult = await _imageService.SaveImage(fileList, "img/user", fileName);

                        if (!saveResult)
                        {
                            return new ResponseModel
                            {
                                Action = Domain.Enums.ActionType.Update,
                                Message = "Lưu ảnh thất bại.",
                                Status = false
                            };
                        }

                        // Không cần lưu thêm thông tin ảnh vào database nếu không có trường Avatar
                    }



                    return new ResponseModel
                    {
                        Action = Domain.Enums.ActionType.Insert,
                        Message = "OK",
                        Status = true
                    };
                }

                errors = string.Join("<br />", identityResult.Errors.Select(x => x.Description));
            }
            else
            {
                // Cập nhật người dùng
                var user = await _userManager.FindByIdAsync(accountDTO.Id);

                if (user == null)
                {
                    return new ResponseModel
                    {
                        Action = Domain.Enums.ActionType.Update,
                        Message = "Người dùng không tồn tại.",
                        Status = false
                    };
                }

                user.Fullname = accountDTO.Fullname;
                user.IsActive = accountDTO.IsActive;
                user.Email = accountDTO.Email;
                user.PhoneNumber = accountDTO.Phone;
                user.Address = accountDTO.Address;

                // Nếu Password được cung cấp, cập nhật mật khẩu
                if (!string.IsNullOrEmpty(accountDTO.Password))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var passwordResult = await _userManager.ResetPasswordAsync(user, token, accountDTO.Password);
                    if (!passwordResult.Succeeded)
                    {
                        errors = string.Join("<br />", passwordResult.Errors.Select(x => x.Description));
                        return new ResponseModel
                        {
                            Action = Domain.Enums.ActionType.Update,
                            Message = $"Lỗi khi cập nhật mật khẩu: {errors}",
                            Status = false
                        };
                    }
                }

                identityResult = await _userManager.UpdateAsync(user);

                if (identityResult.Succeeded)
                {
                    if (accountDTO.Avatar != null)
                    {
                        // Kiểm tra định dạng file
                        var extension = Path.GetExtension(accountDTO.Avatar.FileName).ToLowerInvariant();
                        if (!new[] { ".jpg", ".jpeg", ".png" }.Contains(extension))
                        {
                            return new ResponseModel
                            {
                                Action = Domain.Enums.ActionType.Update,
                                Message = "Chỉ chấp nhận định dạng JPG, JPEG hoặc PNG.",
                                Status = false
                            };
                        }

                        // Đặt tên file: sử dụng ID của người dùng
                        var fileName = $"{accountDTO.Id}{extension}";

                        // Đóng gói Avatar vào danh sách và gọi SaveImage
                        bool saveResult = await _imageService.SaveImage(new List<IFormFile> { accountDTO.Avatar }, "img/user", fileName);

                        if (!saveResult)
                        {
                            return new ResponseModel
                            {
                                Action = Domain.Enums.ActionType.Update,
                                Message = "Lưu ảnh thất bại.",
                                Status = false
                            };
                        }

                        // Nếu lưu thành công, không cần lưu thông tin ảnh vào database vì không có trường Avatar
                    }


                    var hasRole = await _userManager.IsInRoleAsync(user, accountDTO.RoleName);

                    if (!hasRole)
                    {
                        var oldRoles = await _userManager.GetRolesAsync(user);
                        if (oldRoles.Any())
                        {
                            var removeResult = await _userManager.RemoveFromRolesAsync(user, oldRoles);
                            if (!removeResult.Succeeded)
                            {
                                errors = string.Join("<br />", removeResult.Errors.Select(x => x.Description));
                                return new ResponseModel
                                {
                                    Action = Domain.Enums.ActionType.Update,
                                    Message = $"Lỗi khi xóa vai trò cũ: {errors}",
                                    Status = false
                                };
                            }
                        }

                        var addRoleResult = await _userManager.AddToRoleAsync(user, accountDTO.RoleName);
                        if (!addRoleResult.Succeeded)
                        {
                            errors = string.Join("<br />", addRoleResult.Errors.Select(x => x.Description));
                            return new ResponseModel
                            {
                                Action = Domain.Enums.ActionType.Update,
                                Message = $"Lỗi khi thêm vai trò mới: {errors}",
                                Status = false
                            };
                        }
                    }

                    return new ResponseModel
                    {
                        Status = true,
                        Message = "Cập nhật thành công.",
                        Action = Domain.Enums.ActionType.Update
                    };
                }

                errors = string.Join("<br />", identityResult.Errors.Select(x => x.Description));
            }

            return new ResponseModel
            {
                Action = string.IsNullOrEmpty(accountDTO.Id) ? Domain.Enums.ActionType.Insert : Domain.Enums.ActionType.Update,
                Message = string.IsNullOrEmpty(errors) ? "Thực hiện thành công." : $"Thực hiện thất bại. {errors}",
                Status = string.IsNullOrEmpty(errors)
            };
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                user.IsActive = false;
                await _userManager.UpdateAsync(user);
                return true;
            }

            return false;
        }
    }
}
