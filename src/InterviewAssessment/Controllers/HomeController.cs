using Microsoft.AspNetCore.Mvc;

namespace InterviewAssessment.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return NoContent();
    }
}
