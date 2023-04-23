using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VetClinicServer.Model.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public string Email { get; set; } = string.Empty;
        [ForeignKey("RoleId")]
        public int? RoleId { get; set; }
        public Role? Role { get; set; }
        public List<Animal>? Animals { get; set; } 
    }
}
