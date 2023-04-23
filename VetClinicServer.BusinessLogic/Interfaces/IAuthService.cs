using VetClinicServer.Common.Dto;

namespace VetClinicServer.BusinessLogic.Interfaces
{
    public interface IAuthService
    {
        UserDto Authenticate(UserLoginDto userLoginDto);
    }
}
