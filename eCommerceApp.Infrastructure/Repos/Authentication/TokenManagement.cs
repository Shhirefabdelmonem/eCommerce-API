using eCommerceApp.Domain.Entities.Identity;
using eCommerceApp.Domain.Interfaces.Authentication;
using eCommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Infrastructure.Repos.Authentication
{
    public class TokenManagement(AppDbContext context,IConfiguration configuration) : ITokenManagement
    {
        public async Task<int> AddRefreshToken(string userId, string refreshToken)// generate reresh token and save it database

        {
            context.RefreshTokens.Add(new RefreshToken
            {
                UserId = userId,
                Token = refreshToken
            });
            return await context.SaveChangesAsync();
        }

        public string GenerateToken(List<Claim> claims)//generate jwt token
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]!));
            var cred=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddHours(2);
            var token=new JwtSecurityToken(
                issuer: configuration["JWT:Issuer"],
                audience: configuration["JWT:Audience"],
                claims:claims,
                expires:expiration,
                signingCredentials: cred);
            return new JwtSecurityTokenHandler().WriteToken(token); 
                
        }

        public string GetRefreshToken() // generate rondom string 
        {
            const int byteSize = 64;
            byte[] random=new byte[byteSize];
            using(RandomNumberGenerator rng= RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
            }
            string token= Convert.ToBase64String(random);
            return WebUtility.UrlEncode(token);
        }

        public  List<Claim> GetUserClaimsFromToken(string token)
        {
            var tokenHandler=new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            if (jwtToken != null)
                return jwtToken.Claims.ToList();
            else
                return [];
        }

        public async Task<string> GetUserIdByRefreshToken(string refreshToken)
        => (await context.RefreshTokens.FirstOrDefaultAsync(n => n.Token == refreshToken))!.UserId;

        public async Task<int> UpdateRefreshToken(string userId, string refreshToken)
        {
            
            var user = await context.RefreshTokens.FirstOrDefaultAsync(n => n.Token == refreshToken);
            if (user == null) return -1;
            user.Token=refreshToken;
            return await context.SaveChangesAsync();
        
        }

        public async Task<bool> ValidateRefreshToken(string refreshToken)
        {
            var user = await context.RefreshTokens.FirstOrDefaultAsync(n => n.Token == refreshToken);
            return user != null;
        }
    }
}
