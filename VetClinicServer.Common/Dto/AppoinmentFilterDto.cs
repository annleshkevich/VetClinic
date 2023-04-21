using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetClinicServer.Model.Models;

namespace VetClinicServer.Common.Dto
{
   public class AppoinmentFilterDto
    {
        public string Name { get; set; } = string.Empty;
        public string Breed { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
    }
}
