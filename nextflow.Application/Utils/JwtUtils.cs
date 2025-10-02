using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using nextflow.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static nextflow.Application.Utils.JwtUtils;

namespace nextflow.Application.Utils;

public class JwtUtils(IOptions<JwtSettingsUseCase> jwtSettings)
{
    private readonly string _secureKey = jwtSettings.Value.Key;
    public string GenerateToken(User user)
    {
        var key = Encoding.ASCII.GetBytes(_secureKey);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),

            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var tokenConfig = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenConfig);
        return tokenHandler.WriteToken(token);
    }

    public bool ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secureKey);

        try
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false, // Em produção, considere validar (true)
                ValidateAudience = false, // Em produção, considere validar (true)
                ValidateLifetime = true
            };
            tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public class JwtSettingsUseCase
    {
        public required string Key { get; set; }
    }
}