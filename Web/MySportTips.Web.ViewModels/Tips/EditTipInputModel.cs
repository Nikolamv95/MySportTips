namespace MySportTips.Web.ViewModels.Tips
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class EditTipInputModel
    {
        public int TipId { get; set; }

        public string Selection { get; set; }

        public string Description { get; set; }

        public double? Odd { get; set; }

        [Display(Name = "Status")]
        public string StatusName { get; set; }

        [Display(Name = "Progress")]
        public string TimePeriod { get; set; }
    }
}
