namespace InterviewAssessment.Services.Interfaces;

using Microsoft.AspNetCore.Identity;

public interface IJwtService
{
    Task<string> GenerateJwt(IdentityUser user);
}
