using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using CvManager.Data;
using CvManager.Interfaces;
using CvManager.Models;
using CvManager.DTOs;

namespace CvManager.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly BaseContext _context;
        public AuthService(IConfiguration configuration, BaseContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        public string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()!),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(150),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<User> Login(UserDTO user)
        {
            var userLoggin = await _context.Users.FirstOrDefaultAsync(c => c.Email == user.Email);
            if (userLoggin != null && BCrypt.Net.BCrypt.Verify(user.Password, userLoggin.Password))
            {
                return userLoggin;
            }
            return null!;
        }

        public async Task<User> Register(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, 10);
            var userResult = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return userResult.Entity;
        }
    }
}