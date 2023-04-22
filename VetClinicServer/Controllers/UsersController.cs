using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VetClinicServer.BusinessLogic.Interfaces;
using VetClinicServer.Common.Dto;

namespace VetClinicServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
        }
        [HttpGet("List")]
        [Authorize]
        public IActionResult List()
        {
            var result = _userService.AllUsers();
            return Ok(result);
        }
        [HttpPut("Update")]
        [Authorize]
        public IActionResult Put(UserRegistrationDto userRegistrationDto)
        {
            var currentUser = HttpContext.User;
            if (currentUser.FindFirstValue(ClaimTypes.Role) == "Admin"||userRegistrationDto.Id == int.Parse(currentUser.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")))
            {
                return _userService.Update(userRegistrationDto) ? Ok("User has been updated") : BadRequest("User with this login already exists");
            }
            return Forbid();
        }
        [HttpDelete("Delete/{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var currentUser = HttpContext.User;
            if (id == int.Parse(currentUser.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")) || currentUser.FindFirstValue(ClaimTypes.Role) == "Admin")
            {
                return _userService.Delete(id) ? Ok("User has been removed") : BadRequest("User not deleted");
            }
            return Forbid();
        }
    }
}
