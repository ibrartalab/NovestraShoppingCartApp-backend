using System;
using System.ComponentModel.DataAnnotations;
using NShoppingCart.Core.Enums;

namespace NShoppingCart.Application.DTOs;


public class RegisterRequestDto
{
    [Required]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    public string LastName { get; set; } = string.Empty;

    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
    public string Password { get; set; } = string.Empty;

    public string? Role { get; set; }
    public string? PhoneNumber { get; set; }
}
