using VetClinicServer.BusinessLogic.Interfaces;
using VetClinicServer.Common.Dto;

namespace VetClinicServer.BusinessLogic.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHashService _hashService;
        private readonly ITokenService _tokenService;

        public AuthService(IUserRepository userRepository, IHashService hashService, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _hashService = hashService;
            _tokenService = tokenService;
        }

        public async Task<UserDto> AuthenticateAsync(UserLoginDto userLoginDto)
        {
            var user = await _userRepository.GetUserByLoginAsync(userLoginDto.Login);

            if (user == null)
            {
                throw new Exception("Пользователь не найден");
            }

            if (_hashService.VerifyPassordHash(userLoginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new Exception("Неверный пароль");
            }
            var token = _tokenService.GetToken(user);
            var userDto = new UserDto { Login = user.Login, Email = user.Email, Role = new RoleDto { Name = user.Role.Name.ToString() }, AuthorizationHeader = $"Bearer {token}" };
            return userDto;
        }
    }
}
