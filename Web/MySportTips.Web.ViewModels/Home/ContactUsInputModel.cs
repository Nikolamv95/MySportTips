namespace MySportTips.Web.ViewModels.Home
{
    using System.ComponentModel.DataAnnotations;

    public class ContactUsInputModel
    {
        [Required]
        public string FullName { get; set; }

        public string PhoneNum { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string SubjectLine { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
