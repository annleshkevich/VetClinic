using System.Security.Cryptography;
using System.Text;
using VetClinicServer.BusinessLogic.Interfaces;

namespace VetClinicServer.BusinessLogic.Implementations
{
    public class HashService : IHashService
    {
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
        public bool VerifyPassordHash(string password, byte[] passwordSalt, byte[] passwordHash)
        {
            var hmac = new HMACSHA512(passwordSalt);
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return hash.SequenceEqual(passwordHash);
        }
    }
}
