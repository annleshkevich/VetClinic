using VetClinicServer.Common.Dto;
using VetClinicServer.Model.Models;

namespace VetClinicServer.BusinessLogic.Interfaces
{
    public interface IAppointmentService
    {
        IEnumerable<Appointment> AllAppointments();
        List<Appointment> Get(AppointmentDto model);
        Appointment Get(int id);
        bool Create(AppointmentDto model);
        bool Update(AppointmentDto model);
        bool Delete(int id);
    }
}
