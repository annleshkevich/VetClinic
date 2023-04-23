using VetClinicServer.Common.Dto;
using VetClinicServer.Model.Models;

namespace VetClinicServer.BusinessLogic.Interfaces
{
    public interface IAnimalService
    {
        IEnumerable<Animal> AllAnimals();
        Animal Get(int id);
        bool Create(AnimalDto model);
        bool Update(Animal model);
        bool Delete(int id);
    }
}

