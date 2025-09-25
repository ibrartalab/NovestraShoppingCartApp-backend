using System;
using NShoppingCart.Core.Entities;

namespace NShoppingCart.Infrastructure.ExternalServices.JwtGeneration;

public interface IJwtService
{
    Task<string> GenerateToken(User user);
}
