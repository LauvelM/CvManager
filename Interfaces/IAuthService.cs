using CvManager.Models;
using CvManager.DTOs;

namespace CvManager.Interfaces
{
    public interface IAuthService
    {
        Task<User> Register(UserDTO user);
        Task<User> Login(UserDTO user);
        string GenerateToken(User user);
    }
}