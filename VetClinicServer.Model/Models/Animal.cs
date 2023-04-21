using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VetClinicServer.Model.Models
{
    public class Animal
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Breed { get; set; } = string.Empty;
        public int Age { get; set; }
        public string? Img { get; set; } = string.Empty;
        [ForeignKey("UserId")]
        public int? UserId { get; set; }
        public User User { get; set; }= new();
    }
}
