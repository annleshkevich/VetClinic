using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VetClinicServer.Model.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        [ForeignKey("AnimalId")]
        public int? AnimalId { get; set; }
        public Animal Animal { get; set; } 
        public string BehavioralNote { get; set; } = string.Empty;
        public string Complaint { get; set; } = string.Empty;
    }
}
