using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using VetClinicServer.BusinessLogic.Implementations;
using VetClinicServer.BusinessLogic.Interfaces;
using VetClinicServer.Common.Dto;
using VetClinicServer.Model.Models;

namespace VetClinicServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService, ILogger<UserController> logger)
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
        [HttpPost("Create")]
        [Authorize]
        public IActionResult Post(User user)
        {
            return _userService.Create(user) ? Ok("User has been created") : BadRequest("User not created");
        }
        [HttpPut("Update")]
        [Authorize]
        public IActionResult Put(UserRegistrationDto userRegistrationDto)
        {
            var currentUser = HttpContext.User;
            if (userRegistrationDto.Id == int.Parse(currentUser.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")) || currentUser.FindFirstValue(ClaimTypes.Role) == "2")
            {

                return _userService.Update(userRegistrationDto) ? Ok("User has been updated") : BadRequest("User not updated");
            }
            return Forbid();
        }
        [HttpDelete("Delete/{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var currentUser = HttpContext.User;
            if (id == int.Parse(currentUser.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")) || currentUser.FindFirstValue(ClaimTypes.Role) == "2")
            {

                return _userService.Delete(id) ? Ok("User has been removed") : BadRequest("User not deleted");
            }
            return Forbid();
        }
    }
}
