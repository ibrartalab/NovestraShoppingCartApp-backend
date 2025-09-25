using System.ComponentModel.DataAnnotations;

namespace NShoppingCart.Application.DTOs;

public class LoginRequestDto
{
    [Required]
    [StringLength(50)]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}
