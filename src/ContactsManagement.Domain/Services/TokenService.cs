using ContactsManagement.Domain.Models;
using ContactsManagement.Domain.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ContactsManagement.Domain.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly IUsersService _userService;

    public TokenService(IConfiguration configuration, IUsersService userService)
    {
        _configuration = configuration;
        _userService = userService;
    }

    public async Task<string> GetToken(UserDto user)
    {
        var userDb = await _userService.GetUser(user.Username!, user.Password!);

        if (userDb is null || userDb.Username is null)
        {
            return string.Empty;
        }
            
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JwtSettings:Key"));

        var tokenPropriedades = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                    new(ClaimTypes.Name, user.Username!),
                    new(ClaimTypes.Role, (user.Roletype - 1).ToString()),
            }),

            Expires = DateTime.UtcNow.AddMinutes(5),

            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenPropriedades);
        return tokenHandler.WriteToken(token);
    }
}
