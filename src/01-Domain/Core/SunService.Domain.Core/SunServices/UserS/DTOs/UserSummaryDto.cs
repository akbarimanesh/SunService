using SunService.Domain.Core.SunServices.UserS.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Core.SunServices.UserS.DTOs
{
    public  class UserSummaryDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string UserName { get; set; }
        public string ?Mobile { get; set; }
        public string? Email { get; set; }
        public DateTime RegisterAt { get; set; }
        public string City { get; set; }
        public RoleEnum Role { get; set; }
        public bool StatusUser { get; set; } = false;
        public string? ImagePath { get; set; }
    }
}
