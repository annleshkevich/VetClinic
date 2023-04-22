using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VetClinicServer.BusinessLogic.Implementations;
using VetClinicServer.BusinessLogic.Interfaces;
using VetClinicServer.Common.Dto;

namespace VetClinicServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalsController : Controller
    {
        private readonly IAnimalService _animalService;
        public AnimalsController(IAnimalService animalService)
        {
            _animalService = animalService;
        }
        [HttpGet("List")]
        [Authorize]
        public IActionResult List()
        {
            var result = _animalService.AllAnimals();
            return Ok(result);
        }
        [HttpPost("Create")]
        [Authorize]
        public IActionResult Post(AnimalDto animalDto)
        {
            return _animalService.Create(animalDto) ? Ok("Animal has been created") : BadRequest("Animal not created");
        }

        [HttpPut("Update")]
        [Authorize]
        public IActionResult Put(AnimalDto animalDto)
        {
            var currentUser = HttpContext.User;
            var animal = _animalService.Get(animalDto.Id);
            if (animalDto.User.Id == int.Parse(currentUser.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")) || currentUser.FindFirstValue(ClaimTypes.Role) == "2")
            {

                animal.Age = animalDto.Age;
                animal.Img = animalDto.Img;
                animal.User = animalDto.User;
                animal.Breed = animalDto.Breed;
                animal.Name = animalDto.Name;
                return _animalService.Update(animal) ? Ok("Animal has been updated") : BadRequest("Animal not updated");
               
            }
            return Forbid();

        }

        [HttpDelete("Delete/{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var currentUser = HttpContext.User;
            var animal = _animalService.Get(id);
            if (animal.User.Id == int.Parse(currentUser.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")) || currentUser.FindFirstValue(ClaimTypes.Role) == "2")
            {
                return _animalService.Delete(id) ? Ok("Animal has been removed") : BadRequest("Animal not deleted");
            }
            return Forbid();
           
        }
    }
}
