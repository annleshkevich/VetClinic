﻿using Microsoft.EntityFrameworkCore;
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
            //
            return _db.Animals.FirstOrDefault(x => x.Id == id);
        }
        public bool Create(AnimalDto animalDto)
        {
            Animal animal = new Animal();
            animal.Breed = animalDto.Breed;
            animal.Age = animalDto.Age;
            animal.Name = animalDto.Name;
            animal.Img = animalDto.Img;
            animal.UserId = animalDto.UserId;

            _db.Animals.Add(animal);
            return Save();
        }
        public bool Update(Animal animal)
        {
            _db.Update(animal);
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
