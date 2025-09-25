using System;

namespace NShoppingCart.Application.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterUser(RegisterRequestDto registerRequestDto);
    Task<AuthResponseDto> LoginUser(LoginRequestDto loginRequest);
}
