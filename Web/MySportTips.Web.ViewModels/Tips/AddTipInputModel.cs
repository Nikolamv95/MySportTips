namespace MySportTips.Web.ViewModels.Tips
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using MySportTips.Data.Models;

    public class AddTipInputModel
    {
        [Required]
        public int GameId { get; set; }

        [Required]
        public string Selection { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Odd { get; set; }

        [Required]
        [Display(Name = "Status")]
        public string StatusName { get; set; }

        [Required]
        [Display(Name = "Progress")]
        public string TimePeriod { get; set; }

        [Display(Name = "Tag")]
        public string TagName { get; set; }

        public IEnumerable<KeyValuePair<string, string>> TagItems { get; set; }

        public IEnumerable<KeyValuePair<string, string>> SelectionItems { get; set; }
    }
}
