using VetClinicServer.BusinessLogic.Interfaces;
using VetClinicServer.Common.Dto;
using VetClinicServer.Model.Models;

namespace VetClinicServer.BusinessLogic.Implementations
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly IUserService _userService;
        private readonly IHashService _hashService;

        public UserRegistrationService(IUserService userService, IHashService hashService)
        {
            _userService = userService;
            _hashService = hashService;
        }

        public User Register(UserRegistrationDto userRegistrationDto)
        {
            if (_userService.GetByLogin(userRegistrationDto.Login) != null)
            {
                throw new Exception("Пользователь с таким логином уже существует");
            }

            _hashService.CreatePasswordHash(userRegistrationDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Login = userRegistrationDto.Login,
                Email = userRegistrationDto.Email,
                RoleId = 1,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            _userService.Create(user);
            return user;
        }
    }
}
