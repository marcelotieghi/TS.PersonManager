using System.ComponentModel.DataAnnotations;

namespace TS.PersonManager.Core.Enum;

public enum Gender
{
    [Display(Name = "Male")]
    Male,

    [Display(Name = "Female")]
    Female,

    [Display(Name = "Other")]
    Other,

    [Display(Name = "Prefer not to say")]
    PreferNotToSay
}