namespace InterviewAssessment.Controllers;

using Microsoft.AspNetCore.Mvc;
using InterviewAssessment.Models;
using InterviewAssessment.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

public class HomeController : Controller
{
    private readonly IJwtService _jwtService;
    private readonly UserManager<IdentityUser> _userManager;

    /// <inheritdoc />
    public HomeController(IJwtService jwtService, UserManager<IdentityUser> userManager)
    {
        _jwtService = jwtService;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest? request)
    {
        var user = await _userManager.FindByEmailAsync(request?.Email);
        if (user == null)
        {
            return NotFound();
        }

        if (!await _userManager.CheckPasswordAsync(user, request?.Password))
        {
            return Unauthorized();
        }

        var jwt = await _jwtService.GenerateJwt(user);
        return Ok(new LoginResponse {Token = jwt});
    }

    [HttpGet]
    [Authorize]
    public IActionResult VerifyAuthentication()
    {
        return Ok("You are authenticated.");
    }
}
