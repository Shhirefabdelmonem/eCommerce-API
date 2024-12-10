﻿using eCommerceApp.Domain.Entities.Identity;
using eCommerceApp.Domain.Interfaces.Authentication;
using eCommerceApp.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Infrastructure.Repos.Authentication
{
    public class UserManagement(IRoleManagement roleManagement, UserManager<AppUser> userManager, AppDbContext context) : IUserManagement
    {
        public async Task<bool> CreateUser(AppUser user)
        {
            AppUser? _user=await GetUserByEmail(user.Email!);
            if (_user != null) return false;
            return (await userManager.CreateAsync(user, user.PasswordHash!)).Succeeded;

        }

        public async Task<IEnumerable<AppUser>> GetAllUsers() => await context.Users.ToListAsync();

        public async Task<AppUser?> GetUserByEmail(string email)=> await userManager.FindByEmailAsync(email);
       

        public async Task<AppUser> GetUserById(string id)
        {
            var _user = await userManager.FindByIdAsync(id);
            return _user!;
        }

        public async Task<List<Claim>> GetUserClaims(string email)
        {
            var _user = await GetUserByEmail(email);
            string? roleName = await roleManagement.GetUserRole(_user!.Email!);
            List<Claim> claims = [
                new Claim("Fullname",_user!.FullName),
                new Claim(ClaimTypes.NameIdentifier,_user.Id),
                new Claim(ClaimTypes.Email,_user.Email!),
                new Claim(ClaimTypes.Role,roleName!)

                ];
            return claims;
        }

        public async Task<bool> LoginUser(AppUser user)
        {
            var _user = await GetUserByEmail(user.Email!);
            if (_user == null) return false;
            
            var roleName=await roleManagement.GetUserRole(user.Email!);
            if (string.IsNullOrEmpty(roleName)) return false;
            
            return await userManager.CheckPasswordAsync(_user, user.PasswordHash!);

        }

        public async Task<int> RemoveUserByEmail(string email)
        {
            var user= await context.Users.FirstOrDefaultAsync(x => x.Email == email);
            context.Users.Remove(user);
            return await context.SaveChangesAsync();
        }
    }
}
