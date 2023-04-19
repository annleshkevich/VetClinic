using VetClinicServer.Model.Models;

namespace VetClinicServer.Common.Dto
{
    public class AnimalDto
    {
        public string Name { get; set; }
        public string Breed { get; set; }
        public int Age { get; set; }
        public string Img { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}
