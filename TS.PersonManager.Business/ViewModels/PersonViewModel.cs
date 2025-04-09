using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using TS.PersonManager.Core.Enum;

namespace TS.PersonManager.Business.ViewModels;

public class PersonViewModel
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Phone]
    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Date of Birth")]
    public DateTime DateOfBirth { get; set; }

    [Required]
    public Gender Gender { get; set; }

    [Display(Name = "Profile Picture")]
    public IFormFile? ImageUpload { get; set; }

    public string? ImagePath { get; set; }

    public string? ImageName { get; set; }

    public string FullName => $"{FirstName} {LastName}";
}