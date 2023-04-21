using Microsoft.EntityFrameworkCore;
using VetClinicServer.BusinessLogic.Interfaces;
using VetClinicServer.Common.Dto;
using VetClinicServer.Model.Context;
using VetClinicServer.Model.Models;

namespace VetClinicServer.BusinessLogic.Implementations
{
    public class AnimalService : IAnimalService
    {
        public readonly VetClinicContext _db;
        public AnimalService(VetClinicContext db)
        {
            _db = db;
        }
        public IEnumerable<Animal> AllAnimals()
        {
            return _db.Animals.AsNoTracking().ToList();
        }
        public Animal Get(int id)
        {
            return _db.Animals.FirstOrDefault(x => x.Id == id);
        }
        public bool Create(AnimalDto animalDto)
        {
            Animal animal = new Animal();
            animal.Breed = animalDto.Breed;
            animal.Age = animal.Age;
            //animal.Appointments = animal.Appointments;
            animal.Name = animal.Name;
            animal.Img = animal.Img;

            _db.Animals.Add(animal);
            return Save();
        }
        public bool Update(AnimalDto animalDto)
        {
            _db.Update(animalDto);
            return Save();
        }
        public bool Delete(int id)
        {
            var animal = _db.Animals.FirstOrDefault(c => c.Id == id);
            if (animal == null)
                return false;
            _db.Animals.Remove(animal);
            return Save();
        }
        public bool Save()
        {
            var saved = _db.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
