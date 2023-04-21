using VetClinicServer.BusinessLogic.Interfaces;
using VetClinicServer.Common.Dto;
using VetClinicServer.Model.Models;

namespace VetClinicServer.BusinessLogic.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userRepository;
        private readonly IHashService _hashService;
        private readonly ITokenService _tokenService;

        public AuthService(IUserService userRepository, IHashService hashService, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _hashService = hashService;
            _tokenService = tokenService;
        }

        public UserDto Authenticate(UserLoginDto userLoginDto)
        {
            var user = _userRepository.GetByLogin(userLoginDto.Login);

            if (user == null)
            {
                throw new Exception("Пользователь не найден");
            }

            if (_hashService.VerifyPassordHash(userLoginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new Exception("Неверный пароль");
            }
            var token = _tokenService.GetToken(user);
            var userDto = new UserDto { Login = user.Login, Email = user.Email, RoleId = user.RoleId, AuthorizationHeader = $"Bearer {token}" };
            return userDto;
        }
    }
}
