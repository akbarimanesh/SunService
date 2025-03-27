using Microsoft.AspNetCore.Mvc.Rendering;
using SunService.Domain.Core.SunServices.UserS.DTOs;
using SunService.Domain.Core.SunServices.UserS.Entities;

public class UpdateViewModelUser
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Mobile { get; set; }
    public string? Address { get; set; }
    public string? CardNumber { get; set; }
    public string? ShabaNumber { get; set; }
    public int Balance { get; set; }
    public string Email { get; set; }
    public string? Biography { get; set; }
    public IFormFile? ProfileImgFile { get; set; }
    public bool? Status { get; set; }
    public string? HomeServiceTitle { get; set; }
    public int? HomeserviceId { get; set; }
    public List<int>? Selectedhomeservice { get; set; } 
    public List<SelectListItem>? Homeservices { get; set; } 
    public string? UserName { get; set; }
    public int cityId { get; set; }
    public int? RoleId { get; set; }
    public string? ImagePath { get; set; }
    public List<RatingDto>? Ratings { get; set; }
}