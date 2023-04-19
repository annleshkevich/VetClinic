using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetClinicServer.BusinessLogic.Interfaces;
using VetClinicServer.Common.Dto;
using VetClinicServer.Model.Models;

namespace VetClinicServer.BusinessLogic.Implementations
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHashService _hashService;

        public UserRegistrationService(IUserRepository userRepository, IHashService hashService)
        {
            _userRepository = userRepository;
            _hashService = hashService;
        }

        public async Task<User> RegisterAsync(UserRegistrationDto userDto)
        {
            if (await _userRepository.GetUserByLoginAsync(userDto.Login) != null)
            {
                throw new Exception("Пользователь с таким логином уже существует");
            }

            _hashService.CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new User
            {
                Login = userDto.Login,
                Email = userDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = 1
            };

            await _userRepository.CreateUserAsync(user);
            return user;
        }
    }
}
