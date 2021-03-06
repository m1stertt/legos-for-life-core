using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using InnoTech.LegosForLife.Security.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace InnoTech.LegosForLife.Security.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly AuthDbContext _ctx;

        public AuthService(IConfiguration configuration, AuthDbContext ctx)
        {
            _configuration = configuration;
            _ctx = ctx;
        }

        private LoginUser IsValidUserInformation(LoginUser user) {
            return _ctx.LoginUsers.FirstOrDefault(u => u.UserName.Equals(user.UserName) && u.HashedPassword.Equals(user.HashedPassword));
        }

        /// <summary>
        /// Generate JWT Token after successful login.
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public string GenerateJwtToken(LoginUser user)
        {
            var userFound = IsValidUserInformation(user);
            if (userFound == null) return null;
           
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", userFound.Id.ToString()), 
                    new Claim("UserName", userFound.UserName)
                }),
                Expires = DateTime.UtcNow.AddDays(14),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public byte[] VerifyLogin(string username, string password)
        {
            LoginUser user = _ctx.LoginUsers.FirstOrDefault(u => u.UserName.Equals(username));
            if(user==null||!VerifyHash(password,user.HashedPassword,user.PasswordSalt)) return null;
            return user.HashedPassword;
        }

        private bool VerifyHash(string password,byte[] storedHash,byte[] storedSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
        
        public static void CreateHashAndSalt(string password,out byte[] passwordHash,out byte[] salt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                salt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public List<Permission> GetPermissions(int userId)
        {
            return _ctx.UserPermissions
                .Include(up => up.Permission)
                .Where(up => up.UserId == userId)
                .Select(up => up.Permission)
                .ToList();
        }
    }
}