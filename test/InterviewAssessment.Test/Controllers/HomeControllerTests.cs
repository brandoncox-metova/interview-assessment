namespace InterviewAssessment.Test.Controllers
{
    using InterviewAssessment.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Xunit;

    public class HomeControllerTests
    {
        private readonly HomeController _homeController;

        public HomeControllerTests()
        {
            _homeController = new HomeController();
        }

        [Fact]
        public void Test_Index()
        {
            var response = _homeController.Index();

            Assert.IsType<NoContentResult>(response);
        }
    }
}