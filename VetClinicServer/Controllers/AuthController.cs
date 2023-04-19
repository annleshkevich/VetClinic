using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VetClinicServer.BusinessLogic.Interfaces;
using VetClinicServer.Common.Dto;

namespace VetClinicServer.Controllers
{

    [ApiController]
    [Route("public")]
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IUserRegistrationService _userRegistrationService;

        public AuthController(IAuthService authService, IUserRegistrationService userRegistrationService)
        {
            _authService = authService;
            _userRegistrationService = userRegistrationService;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Get(UserLoginDto userDto)
        {
            try
            {
                var user = await _authService.AuthenticateAsync(userDto);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Registration")]
        public async Task<IActionResult> Create(UserRegistrationDto user)
        {
            try
            {
                await _userRegistrationService.RegisterAsync(user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
