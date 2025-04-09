using TS.PersonManager.Core.Enum;

namespace TS.PersonManager.Core.Entities;

public sealed record Person
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public Gender? Gender { get; set; }
    public string ImageName { get; set; } = string.Empty;
}