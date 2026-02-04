using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public static class TestJwtTokens
{
    // MUST match your API's JWT configuration
    private const string Issuer = "SecureTaskApi";
    private const string Audience = "SecureTaskApiUsers";
    private const string SecretKey = "THIS_IS_A_TEST_SECRET_KEY_123456";

    public static string ValidUserToken => GenerateToken();

    private static string GenerateToken()
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, "test-user-id"),
            new Claim(ClaimTypes.Name, "test-user"),
            new Claim(ClaimTypes.Role, "User")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: Issuer,
            audience: Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}