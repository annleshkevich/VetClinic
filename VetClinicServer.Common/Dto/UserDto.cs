using VetClinicServer.Model.Models;

namespace VetClinicServer.Common.Dto
{
    public class UserDto
    {
        public int Id { get; set; } 
        public string Login { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int? RoleId { get; set; }
        public string AuthorizationHeader { get; set; } = string.Empty;
        public List<Animal>? Animals { get; set; }
    }
}
