using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace TEcomerc.Api.Controllers;

sealed record AuthForm(string user, string password);

[ApiController]
[AllowAnonymous]
[Route("auth/login")]
[Produces("application/json")]
sealed class AuthController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Post([FromBody] AuthForm form)
    {
        if ( form.user != "dev@martech.com"
          || form.password != "Senha@123")
        {
            return Unauthorized();
        }

         var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("this-is-a-test-secret-key-at-least-32-chars")
        );

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            audience: "MyApi",
            issuer: "MyApi",
            claims: [
                new Claim(ClaimTypes.Name, form.user)
            ],
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new
        {
            JWT = jwt
        });
    }
}