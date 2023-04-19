using Microsoft.EntityFrameworkCore;
using VetClinicServer.Common.Dto;
using VetClinicServer.Model.Models;

namespace VetClinicServer.BusinessLogic.Interfaces
{
    public interface IUserRepository
    {
        Task CreateUserAsync(User user);
        Task<User> GetUserByIdAsync(int userId);
        Task<User> GetUserByLoginAsync(string login);
        Task<User> GetUserByEmailAsync(string email);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(User user);
    }
}
