using Microsoft.AspNetCore.Http;
using SunService.Domain.Core.SunServices.UserS.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunService.Domain.Core.SunServices.UserS.DTOs
{
    public class UserDto
    {
        #region Properties

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? RePassword { get; set; }
        public string Mobile { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public bool Status {  get; set; }
        public int CityId { get; set; }
        public int RoleId   { get; set; }
        public RoleEnum Role { get; set; }
        public IFormFile? ProfileImgFile { get; set; }
        public string? Biography { get; set; }
        public string? ImagePath { get; set; }
        #endregion

    }
}

