namespace VetClinicServer.Common.Dto
{
   public class AppointmentFilterDto
    {
        public string? Name { get; set; } = string.Empty;
        public string? Breed { get; set; } = string.Empty;
        public DateTime? DateCreated { get; set; }
    }
}
