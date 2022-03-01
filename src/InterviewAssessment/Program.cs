using InterviewAssessment.Models.Settings;
using InterviewAssessment.Services.Implementations;
using InterviewAssessment.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();

builder.Services.AddScoped<IWeatherService, WeatherService>();

builder.Services.Configure<WeatherSettings>(builder.Configuration.GetSection(nameof(WeatherSettings)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
