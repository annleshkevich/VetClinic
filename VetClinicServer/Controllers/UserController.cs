using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VetClinicServer.BusinessLogic.Interfaces;
using VetClinicServer.Model.Models;

namespace VetClinicServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository, ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        [HttpGet("GetUser/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(id);
                return Ok(user);
            }
            catch (Exception ex)
            {

                _logger.LogInformation(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("PutUser/{id}")]
        public async Task<IActionResult> Update(User user)
        {
            try
            {
                var existingUser = await _userRepository.GetUserByIdAsync(user.Id);
                if (existingUser == null)
                {
                    return NotFound();
                }

                existingUser.Login = user.Login;
                existingUser.Email = user.Email;
                existingUser.RoleId = user.RoleId;
                existingUser.Role = null;
                await _userRepository.UpdateUserAsync(existingUser);

                return Ok(existingUser);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                await _userRepository.DeleteUserAsync(user);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);

                return BadRequest(ex.Message);
            }
        }
    }
}
