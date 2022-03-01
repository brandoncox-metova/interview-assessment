namespace InterviewAssessment.Services.Implementations;

using InterviewAssessment.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class JwtService : IJwtService
{
    private readonly UserManager<IdentityUser> _userManager;

    public JwtService(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    /// <inheritdoc />
    public async Task<string> GenerateJwt(IdentityUser user)
    {
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                "F2YPUoBga6K3rsviiwKpvSgc9YWDkn6S6r2Gc4KZMaG4i3JDo4d6GF25wtJAA8wYjkxDVXEsE9FPQVz9FmPbRAvRE2p8Eq4FhTnncoLUFfaret8GFf89JGLUQn3tnDes"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var userRoles = await _userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Email, user.UserName), new(JwtRegisteredClaimNames.NameId, user.Id),
        };

        authClaims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));

        var token = new JwtSecurityToken(
            "metova.com",
            null,
            authClaims,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
