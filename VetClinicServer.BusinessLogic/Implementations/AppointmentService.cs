using Microsoft.EntityFrameworkCore;
using VetClinicServer.BusinessLogic.Interfaces;
using VetClinicServer.Common.Dto;
using VetClinicServer.Model.Context;
using VetClinicServer.Model.Models;

namespace VetClinicServer.BusinessLogic.Implementations
{
    public class AppointmentService : IAppointmentService
    {
        public readonly VetClinicContext _db;
        public AppointmentService(VetClinicContext db)
        {
            _db = db;
        }
        public IEnumerable<Appointment> AllAppointments()
        {
            return _db.Appointments.AsNoTracking().ToList();
        }
        public Appointment Get(int id)
        {
            return _db.Appointments.FirstOrDefault(x=>x.Id==id);
        }

        public List<Appointment> Get(AppointmentDto model)
        {
            var list = _db.Appointments.AsQueryable();
            if (model.Animal.Breed is not null)
            {
                list = list.Where(x => x.Animal.Breed.Contains(model.Animal.Breed.ToLower()));
            }
            if (model.Animal.Name is not null)
            {
                list = list.Where(x => x.Animal.Name.Contains(model.Animal.Name.ToLower()));
            }
            if (model.CreatedDate != null)
            {
                list = list.Where(x => x.CreatedDate == model.CreatedDate);
            }
            return list.ToList();
        }
        public bool Create(AppointmentDto appointmentDto)
        {
            Appointment appointment = new Appointment();
            appointment.CreatedDate = appointmentDto.CreatedDate;
            appointment.BehavioralNote = appointmentDto.BehavioralNote;
            appointment.Animal = appointmentDto.Animal;
            appointment.Complaint = appointment.Complaint;
            _db.Appointments.Add(appointment);
            return Save();
        }
        public bool Update(AppointmentDto appointmentDto)
        {
            _db.Update(appointmentDto);
            return Save();
        }
        public bool Delete(int id)
        {
            var appointment = _db.Appointments.FirstOrDefault(c => c.Id == id);
            if (appointment == null)
                return false;
            _db.Appointments.Remove(appointment);
            return Save();
        }
        public bool Save()
        {
            var saved = _db.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
