using Microsoft.EntityFrameworkCore;
using VetClinicServer.BusinessLogic.Interfaces;
using VetClinicServer.Common.Dto;
using VetClinicServer.Model.Context;
using VetClinicServer.Model.Models;

namespace VetClinicServer.BusinessLogic.Implementations
{
    public class UserService : IUserService
    {
        private readonly VetClinicContext _db;
        private readonly IHashService _hashService;
        public UserService(VetClinicContext db, IHashService hashService)
        {
            _db = db;
            _hashService = hashService;

        }
        public IEnumerable<User> AllUsers()
        {
            return _db.Users.AsNoTracking().ToList();
        }
        public bool Create(User user)
        {
            _db.Users.Add(user);
            return Save();
        }
        public User GetByLogin(string login)
        {
            return _db.Users.Include(x => x.Role).AsNoTracking().FirstOrDefault(u => u.Login == login);
        }
        public User GetById(int id)
        {
            return _db.Users.Include(x => x.Role).AsNoTracking().FirstOrDefault(x => x.Id == id);
        }
        public bool Update(UserRegistrationDto userRegistrationDto)
        {
            if (GetByLogin(userRegistrationDto.Login) != null && userRegistrationDto.Login != _db.Users.Find(userRegistrationDto.Id).Login)
            {
                throw new Exception("Пользователь с таким логином уже существует");
            }
            else
            {
                var user = GetById(userRegistrationDto.Id);
                _hashService.CreatePasswordHash(userRegistrationDto.Password, out byte[] passwordHash, out byte[] passwordSalt);


                user.Login = userRegistrationDto.Login;
                user.Email = userRegistrationDto.Email;
                user.RoleId = 1;
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                _db.Update(user);
            }

            return Save();
        }
        public bool Delete(int id)
        {
            var user = _db.Users.FirstOrDefault(c => c.Id == id);
            if (user == null)
                return false;
            _db.Users.Remove(user);
            return Save();
        }
        public bool Save()
        {
            var saved = _db.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
