using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Numerics;
using System.Security.Claims;
using VetClinicServer.BusinessLogic.Implementations;
using VetClinicServer.BusinessLogic.Interfaces;
using VetClinicServer.Common.Dto;
using VetClinicServer.Model.Models;

namespace VetClinicServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet("List")]
        [Authorize]
        public IActionResult List()
        {
            var result = _appointmentService.AllAppointments();
            return Ok(result);
        }
        [HttpGet("Find")]
        [Authorize]
        public IActionResult Get(AppoinmentFilterDto appointment)
        {
            var result = _appointmentService.Get(appointment);
            return Ok(result);
        }

        [HttpPost("Create")]
        [Authorize]
        public IActionResult Post(AppointmentDto appointment)
        {
            return _appointmentService.Create(appointment) ? Ok("Appointment has been created") : BadRequest("Appointment not created");
        }

        [HttpPut("Update")]
        [Authorize]
        public IActionResult Put(AppointmentDto appointmentDto)
        {
            var currentUser = HttpContext.User;
            var appointment = _appointmentService.Get(appointmentDto.Id);
            if (appointment.Animal.UserId != int.Parse(currentUser.FindFirstValue("sub")) || currentUser.FindFirstValue(ClaimTypes.Role) != "2")
            {
                return Forbid();
            }
            appointment.BehavioralNote = appointment.BehavioralNote;
            appointment.CreatedDate = appointmentDto.CreatedDate;
            //дописать пораметры
            return _appointmentService.Update(appointment) ? Ok("Appointment has been updated") : BadRequest("Appointment not updated");
        }

        [HttpDelete("Delete/{id}")]
        [Authorize]
        public IActionResult DeleteForUser(int id)
        {
            var currentUser = HttpContext.User;
            var appointment = _appointmentService.Get(id);
            if (appointment.Animal.UserId == int.Parse(currentUser.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")) || currentUser.FindFirstValue(ClaimTypes.Role) == "2")
            {
            return _appointmentService.Delete(id) ? Ok("Appointment has been removed") : BadRequest("Appointment not deleted");

            }
            return Forbid();
        }
    }
}
