using VetClinicServer.Model.Models;

namespace VetClinicServer.Common.Dto
{
    public class UserDto
    {
        public string Login { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int RoleId { get; set; } 
        public Role Role { get; set; }
        public string AuthorizationHeader { get; set; } = string.Empty;
        public List<Animal> Animals { get; set; }
    }
}
