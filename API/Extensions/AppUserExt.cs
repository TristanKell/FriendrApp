using System;
using API.DTOs;
using API.Entitites;
using API.Interfaces;
namespace API.Extensions;

public static class AppUserExt
{
    public static UserDTO ToDTO(this AppUser user, ITokenService tokenService)
    {
        return new UserDTO
        {
            Id = user.Id,
            DisplayName = user.DisplayName,
            Email = user.Email,
            Token = tokenService.CreateToken(user)
        };
    }
}