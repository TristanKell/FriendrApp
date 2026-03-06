using System;
using API.Entitites;
using API.Interfaces;

namespace API.Services;

public class TokenService : ITokenService
{
    string ITokenService.CreateToken(AppUser user)
    {
        throw new NotImplementedException();
    }
}
