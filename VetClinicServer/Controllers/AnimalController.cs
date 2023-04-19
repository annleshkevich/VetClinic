using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using VetClinicServer.BusinessLogic.Interfaces;

namespace VetClinicServer.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AnimalController : Controller
    {
        private readonly IAnimalService _animalService;
        public AnimalController(IAnimalService animalService)
        {
            _animalService = animalService;
        }

       
    }
}
