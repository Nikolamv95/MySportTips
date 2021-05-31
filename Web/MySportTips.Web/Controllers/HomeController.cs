namespace MySportTips.Web.Controllers
{
    using System.Diagnostics;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using MySportTips.Common;
    using MySportTips.Services.Messaging;
    using MySportTips.Web.ViewModels;
    using MySportTips.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly IEmailSender emailSender;

        public HomeController(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult ContactUs()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactUs(ContactUsInputModel contactUsInput)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var content = new StringBuilder();
            content.AppendLine($"User with email: {contactUsInput.Email} sent the following message:");
            content.AppendLine($"{contactUsInput.Content}");

            await this.emailSender.SendEmailAsync(GlobalConstants.SystemEmail, contactUsInput.FullName,
                GlobalConstants.SystemEmail, contactUsInput.SubjectLine, content.ToString());

            this.TempData["Message"] = "The email was sent successfully.";
            return this.RedirectToAction(nameof(this.Index));
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
            else if (errorCode == 403)
            {
                return this.RedirectToAction(nameof(this.AccessDenied));
            }
            else
            {
                return this.Redirect("Error");
            }
        }

        public IActionResult AccessDenied()
        {
            return this.View();
        }
    }
}
