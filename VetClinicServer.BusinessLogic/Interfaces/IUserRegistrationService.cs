using VetClinicServer.Common.Dto;
using VetClinicServer.Model.Models;

namespace VetClinicServer.BusinessLogic.Interfaces
{
    public interface IUserRegistrationService
    {
        Task<User> RegisterAsync(UserRegistrationDto userDto);
    }
}
