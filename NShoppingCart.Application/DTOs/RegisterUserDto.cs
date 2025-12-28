using System;
using System.ComponentModel.DataAnnotations;

namespace NShoppingCart.Application.DTOs;

public class RegisterRequestDto
{
    [Required]
    public string FullName { get; set; } = string.Empty;
    [Required]
    public string UserName { get; set; } = string.Empty;

    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
    public string Password { get; set; } = string.Empty;

    public string? PhoneNumber { get; set; }
}
