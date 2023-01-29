using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using AluraChallenge.VideoSharingPlatform.Services.Identity.API.Models;
using static System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace AluraChallenge.VideoSharingPlatform.Services.Identity.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;

    public AccountController(
        UserManager<IdentityUser> userManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    // POST api/v1/Account/Register
    [HttpPost("Register")]
    [ProducesResponseType(Status200OK)]
    [ProducesResponseType(Status400BadRequest)]
    public async Task<ActionResult<LoginResponse>> Register([FromBody] RegisterUserCommand command)
    {
        var user = new IdentityUser
        {
            Email = command.Email,
            UserName = command.Email
        };

        var result = await _userManager.CreateAsync(user, command.Password);

        if (result.Succeeded)
        {
            return GenerateJwt(user);
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(error.Code, error.Description);
        }

        return ValidationProblem(ModelState);
    }

    // POST api/v1/Account/Login
    [HttpPost("Login")]
    [ProducesResponseType(Status200OK)]
    [ProducesResponseType(Status400BadRequest)]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginUserCommand command)
    {
        var user = await _userManager.FindByEmailAsync(command.Email);

        if (user != null && await _userManager.CheckPasswordAsync(user, command.Password))
        {
            return GenerateJwt(user);
        }

        return Problem("Invalid credentials", statusCode: Status400BadRequest);
    }

    private LoginResponse GenerateJwt(IdentityUser user)
    {
        var claims = new List<Claim>
        {
            new(Sub, user.Id),
            new(Email, user.Email!),
            new(Jti, Guid.NewGuid().ToString()),
            new(UniqueName, user.UserName!)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.UtcNow.AddDays(1);

        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: expiration,
            signingCredentials: creds
        );

        return new LoginResponse
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            ExpiresIn = expiration
        };
    }
}
