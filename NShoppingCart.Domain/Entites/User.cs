using System.ComponentModel.DataAnnotations;
using NShoppingCart.Core.Enums;


namespace NShoppingCart.Core.Entities;

public class User : BaseEntity
{
    [Required]
    [StringLength(50)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    [StringLength(10)]
    public string Role { get; set; } = UserRole.User; // User, Admin

    [StringLength(20)]
    public string? PhoneNumber { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime? LastLoginAt { get; set; }

    // Navigation properties
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}

