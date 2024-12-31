using BookSale.Management.Application.Abstracts;
using BookSale.Management.Application.DTOs;
using BookSale.Management.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Management.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthenticationService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ResponseModel> CheckLogin(string username, string password, bool hasRemember)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user is null)
            {
                return new ResponseModel
                {
                    Status = false,
                    Message = "Username or password is incorrect"
                };
            }

            var result = await _signInManager.PasswordSignInAsync(user, password, isPersistent: hasRemember, lockoutOnFailure: true);

            if (result.IsLockedOut)
            {
                var remainingLockout = user.LockoutEnd.Value - DateTimeOffset.UtcNow;

                return new ResponseModel
                {
                    Message = $"User is locked out. Please try again after {Math.Round(remainingLockout.TotalMinutes)} minutes"
                };
            }

            if (result.Succeeded)
            {
                await _userManager.ResetAccessFailedCountAsync(user);
                return new ResponseModel
                {

                    Status = true
                };


            }


            return new ResponseModel
            {
                Status = false,
                Message = "Username or password is incorrect"

            };


        }
    }
}
