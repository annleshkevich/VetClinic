using VetClinicServer.Model.Models;

namespace VetClinicServer.BusinessLogic.Interfaces
{
    public interface IAnimalService
    {
        IEnumerable<Animal> AllAnimals();
    }
}
