namespace MySportTips.Web.ViewModels.Users
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class EditUserInputModel
    {
        public string UserId { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }

        public string Email { get; set; }

        [Display(Name = "Current role")]
        public ICollection<string> Roles { get; set; }

        [Display(Name = "Choose old role")]
        public string OldRole { get; set; }

        [Display(Name = "Assign new role")]
        [Required]
        public string NewRole { get; set; }
    }
}
