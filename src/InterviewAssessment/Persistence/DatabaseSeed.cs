namespace InterviewAssessment.Persistence;

using Microsoft.AspNetCore.Identity;

public class DatabaseSeed
{
    public static async Task Seed(UserManager<IdentityUser> userManager)
    {
        if (await userManager.FindByEmailAsync("default.user@metova.com") != null)
        {
            return;
        }

        await userManager.CreateAsync(
            new IdentityUser {Email = "default.user@metova.com", UserName = "default.user@metova.com" },
            "verySecretPassword1234!");
    }
}
