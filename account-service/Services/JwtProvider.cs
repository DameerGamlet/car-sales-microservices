using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

internal sealed class JwtProvider : IJwtProvider
{
    private readonly JwtOptions options;

    public JwtProvider(IOptions<JwtOptions> options)
    {
        this.options = options.Value;
    }

    public string GenerateJwtToken(Account account)
    {
        var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, account.UserId.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, account.Email)
    };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySecretKeyWithAtLeast32Characters"));
        // var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "car-sale@gmail.com",
            // issuer: options.Issuer,
            // audience: options.Audience,
            audience: "car-sale.com",
            claims: claims,
            expires: DateTime.Now.AddMonths(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}