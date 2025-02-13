using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Core.SunServices.UserS.DTOs
{
    public class ExpertDto
    {
        public int Id { get; set; }
        public string ExpertFullName { get; set; }
        public string Biography { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }

    }
}
