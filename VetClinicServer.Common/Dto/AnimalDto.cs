using VetClinicServer.Model.Models;

namespace VetClinicServer.Common.Dto
{
    public class AnimalDto
    {
        public int Id { get; set; }   
        public string Name { get; set; } = string.Empty;
        public string Breed { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Img { get; set; } = string.Empty;
        public int UserId { get; set; }
        //public User User { get; set; }
    }
}
