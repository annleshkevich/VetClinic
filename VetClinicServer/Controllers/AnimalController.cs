using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using VetClinicServer.BusinessLogic.Implementations;
using VetClinicServer.BusinessLogic.Interfaces;
using VetClinicServer.Common.Dto;
using VetClinicServer.Model.Models;

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

        [HttpPost("Create")]
        [Authorize]
        public IActionResult Post(AnimalDto animal)
        {
            var currentUser = HttpContext.User;
            if (animal.User.Id != int.Parse(currentUser.FindFirstValue(JwtRegisteredClaimNames.NameId)) || currentUser.FindFirstValue(ClaimTypes.Role) != "2")
            {
                return Forbid();
            }
            return _animalService.Create(animal) ? Ok("Animal has been created") : BadRequest("Animal not created");
        }

        [HttpPut("Update")]
        [Authorize]
        public IActionResult Put(AnimalDto animal)
        {
            var currentUser = HttpContext.User;
            if (animal.User.Id != int.Parse(currentUser.FindFirstValue(JwtRegisteredClaimNames.NameId)) || currentUser.FindFirstValue(ClaimTypes.Role) != "2")
            {
                return Forbid();
            }
            return _animalService.Update(animal) ? Ok("Animal has been updated") : BadRequest("Animal not updated");
        }

        [HttpDelete("Delete/{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var currentUser = HttpContext.User;
            var animal = _animalService.Get(id);
            if (animal.User.Id != int.Parse(currentUser.FindFirstValue(JwtRegisteredClaimNames.NameId)) || currentUser.FindFirstValue(ClaimTypes.Role) != "2")
            {
                return Forbid();
            }
            return _animalService.Delete(id) ? Ok("Animal has been removed") : BadRequest("Animal not deleted");
        }
    }
}
