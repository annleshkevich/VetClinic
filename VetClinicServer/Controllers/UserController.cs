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
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("Create")]
        [Authorize]
        public IActionResult Post(User user)
        {
            return _userService.Create(user) ? Ok("User has been created") : BadRequest("User not created");
        }
        [HttpPut("Update/{id}")]
        [Authorize]
        public IActionResult Put(User user)
        {
            var currentUser = HttpContext.User;
            if (user.Id != int.Parse(currentUser.FindFirstValue(JwtRegisteredClaimNames.NameId)) || currentUser.FindFirstValue(ClaimTypes.Role) != "2")
            {
                return Forbid();
            }
            return _userService.Update(user) ? Ok("User has been updated") : BadRequest("User not updated");
        }
        [HttpDelete("Delete {id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var currentUser = HttpContext.User;
            if (id != int.Parse(currentUser.FindFirstValue(JwtRegisteredClaimNames.NameId)) || currentUser.FindFirstValue(ClaimTypes.Role) != "2")
            {
                return Forbid();
            }
            return _userService.Delete(id) ? Ok("User has been removed") : BadRequest("User not deleted");
        }
    }
}
