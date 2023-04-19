using VetClinicServer.Common.Dto;

namespace VetClinicServer.BusinessLogic.Interfaces
{
    public interface IAuthService
    {
        Task<UserDto> AuthenticateAsync(UserLoginDto userLoginDto);
    }
}
