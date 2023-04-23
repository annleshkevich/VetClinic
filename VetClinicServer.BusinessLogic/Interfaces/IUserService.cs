using Microsoft.EntityFrameworkCore;
using VetClinicServer.Common.Dto;
using VetClinicServer.Model.Models;

namespace VetClinicServer.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> AllUsers();
        bool Create(User user);
        User GetByLogin(string login);
        User GetById(int id);
        bool Update(UserRegistrationDto userRegistrationDto);
        bool Delete(int id);
    }
}
