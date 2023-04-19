using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using VetClinicServer.BusinessLogic.Implementations;
using VetClinicServer.BusinessLogic.Interfaces;
using VetClinicServer.Common.Dto;
using VetClinicServer.Model.Models;

namespace VetClinicServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
            var appointments = _appointmentService.AllAppointments();
            return Ok(appointments);
        }
        [HttpGet("Find")]
        [Authorize]
        public IActionResult Get (AppointmentDto appointment)
        {
           var result =  _appointmentService.Get(appointment);
            return Ok(result);
        }
    }
}
