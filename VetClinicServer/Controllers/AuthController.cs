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
        public IActionResult Get(UserLoginDto userDto)
        {
            try
            {
                var user = _authService.Authenticate(userDto);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Registration")]
        public IActionResult Create(UserRegistrationDto user)
        {
            try
            {
                _userRegistrationService.Register(user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
