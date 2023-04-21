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
            return _db.Appointments.Include(x => x.Animal).FirstOrDefault(x => x.Id == id);
        }

        public List<Appointment> Get(AppointmentFilterDto model)
        {
            var list = _db.Appointments.Include(x => x.Animal).AsQueryable();
            if (!String.IsNullOrEmpty(model.Breed))
            {
                list = list.Where(x => x.Animal.Breed == model.Breed.ToLower());
            }
            if (!String.IsNullOrEmpty(model.Name))
            {
                list = list.Where(x => x.Animal.Name.Contains(model.Name.ToLower()));
            }
            if (model.DateCreated != null)
            {
                list = list.Where(x => x.CreatedDate == model.DateCreated);

            }
            return list.ToList();
        }
        public bool Create(AppointmentDto appointmentDto)
        {
            Appointment appointment = new Appointment();
            appointment.CreatedDate = appointmentDto.CreatedDate;
            appointment.BehavioralNote = appointmentDto.BehavioralNote;
            appointment.AnimalId = appointmentDto.AnimalId;

            Animal? animal = _db.Animals.Find(appointmentDto.AnimalId);
            appointment.Animal = animal;

            appointment.Complaint = appointmentDto.Complaint;
            _db.Appointments.Add(appointment);
            return Save();
        }
        public bool Update(Appointment appointment)
        {
            _db.Update(appointment);
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
