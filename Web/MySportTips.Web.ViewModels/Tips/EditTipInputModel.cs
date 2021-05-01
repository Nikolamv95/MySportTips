namespace MySportTips.Web.ViewModels.Tips
{
    using System.ComponentModel.DataAnnotations;

    public class EditTipInputModel
    {
        public int TipId { get; set; }

        [Required]
        public string Selection { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Odd { get; set; }

        [Display(Name = "Status")]
        public string StatusName { get; set; }

        [Display(Name = "Progress")]
        public string TimePeriod { get; set; }
    }
}
