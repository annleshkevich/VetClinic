using VetClinicServer.Model.Models;

namespace VetClinicServer.Common.Dto
{
    public class AppointmentDto
    {
        public DateTime CreatedDate { get; set; }
        public int AnimalId { get; set; }
        public Animal Animal { get; set; }
        public string BehavioralNote { get; set; }
        public string Complaint { get; set; }
    }
}
