namespace VetClinicServer.BusinessLogic.Interfaces
{
    public interface IHashService
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPassordHash(string password, byte[] passwordSalt, byte[] passwordHash);
    }
}
