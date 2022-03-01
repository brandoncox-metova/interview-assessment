using InterviewAssessment.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using InterviewAssessment.Services.Implementations;
using InterviewAssessment.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
       .AddEntityFrameworkStores<ApplicationDbContext>()
       .AddUserManager<UserManager<IdentityUser>>();

builder.Services.Configure<IdentityOptions>(
    options =>
    {
        // Password settings.
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 8;
        options.Password.RequiredUniqueChars = 1;

        // Lockout settings.
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.AllowedForNewUsers = true;

        // User settings.
        options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        options.User.RequireUniqueEmail = false;
    });

builder.Services.AddAuthentication(
            o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
       .AddJwtBearer(
            o =>
            {
                var securityKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        "F2YPUoBga6K3rsviiwKpvSgc9YWDkn6S6r2Gc4KZMaG4i3JDo4d6GF25wtJAA8wYjkxDVXEsE9FPQVz9FmPbRAvRE2p8Eq4FhTnncoLUFfaret8GFf89JGLUQn3tnDes"));
                o.TokenValidationParameters.ValidIssuer = "metova.com";
                o.TokenValidationParameters.ValidateAudience = false;
                o.TokenValidationParameters.ValidateIssuer = true;
                o.TokenValidationParameters.ValidateLifetime = true;
                o.TokenValidationParameters.IssuerSigningKey = securityKey;
                o.TokenValidationParameters.ValidAlgorithms = new List<string> {SecurityAlgorithms.HmacSha256};
            });

builder.Services.AddScoped<IJwtService, JwtService>();

var app = builder.Build();

// Seed database
var userManager = app.Services.CreateScope().ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
await DatabaseSeed.Seed(userManager);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
