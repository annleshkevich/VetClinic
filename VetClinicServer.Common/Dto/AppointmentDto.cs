using VetClinicServer.Model.Models;

namespace VetClinicServer.Common.Dto
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? AnimalId { get; set; }
        public string BehavioralNote { get; set; } = string.Empty;
        public string Complaint { get; set; } = string.Empty;
    }
}
