using VetClinicServer.Common.Dto;
using VetClinicServer.Model.Models;

namespace VetClinicServer.BusinessLogic.Interfaces
{
    public interface IAppointmentService
    {
        IEnumerable<Appointment> AllAppointments();
        List<Appointment> Get(AppointmentFilterDto model);
        Appointment Get(int id);
        bool Create(AppointmentDto model);
        bool Update(Appointment model);
        bool Delete(int id);
    }
}
