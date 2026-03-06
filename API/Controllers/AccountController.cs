using System;
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entitites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(AppDbContext context) : BaseApiController
{
    [HttpPost("register")] // api/account/register

    public async Task<ActionResult<AppUser>> Register(RegisterDTO registerDTO)
    {

        if (await EmailExists(registerDTO.Email)) return BadRequest("email taken");



        using var hmac = new HMACSHA512();

        var user = new AppUser
        {
            DisplayName = registerDTO.DisplayName,
            Email = registerDTO.Email,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
            PasswordSalt = hmac.Key
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return user;
    }

    [HttpPost("login")]    
    public async Task<ActionResult<AppUser>> Login(LoginDTO loginDTO)
    {
        var user = await context.Users.SingleOrDefaultAsync( x => x.Email == loginDTO.Email);

        if (user == null) return Unauthorized("Invalid email adress");

        using var hmac = new HMACSHA512(user.PasswordSalt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));

        for (var i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("invalid password");
        }

        return user;
    }

    private async Task<bool> EmailExists(string Email)
    {
        return await context.Users.AnyAsync(x => x.Email.ToLower() == Email.ToLower());
    }

}
