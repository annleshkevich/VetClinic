using VetClinicServer.Common.Dto;
using VetClinicServer.Model.Models;

namespace VetClinicServer.BusinessLogic.Interfaces
{
    public interface IAppointmentService
    {
        IEnumerable<Appointment> AllAppointments();
        List<Appointment> Get(AppointmentDto model);
    }
}
