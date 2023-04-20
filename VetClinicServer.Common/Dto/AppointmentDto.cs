using VetClinicServer.Model.Models;

namespace VetClinicServer.Common.Dto
{
    public class AppointmentDto
    {
        public DateTime CreatedDate { get; set; }
        public Animal Animal { get; set; } = new Animal();
        public string BehavioralNote { get; set; } = string.Empty;
        public string Complaint { get; set; } = string.Empty;
    }
}
