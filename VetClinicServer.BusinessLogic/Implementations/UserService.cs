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
        public UserService(VetClinicContext db)
        {
            _db= db;
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
            return _db.Users.FirstOrDefault(x => x.Id == id);
        }
        public bool Update(User user)
        {
            _db.Update(user);
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
