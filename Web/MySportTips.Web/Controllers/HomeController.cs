namespace MySportTips.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using MySportTips.Web.ViewModels;

    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult ContactUs()
        {
            return this.View();
        }

        public IActionResult Subscribe()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        public IActionResult StatusCodeError(int errorCode)
        {
            if (errorCode == 404)
            {
                return this.View();
            }
            else
            {
                return this.Redirect("Error");
            }
        }
    }
}
