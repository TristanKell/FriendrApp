using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class RegisterDTO
{
    [Required]
    public string DisplayName { get; set; } = "";


    [Required]
    [EmailAddress]
    public string Email { get; set; } = "";


    [Required]
    [PasswordPropertyText]
    [MinLength(6)]
    public string Password { get; set; } = "";


}
