namespace MySportTips.Web.ViewModels.Games
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MySportTips.Common;

    public class AddGameInputModel
    {
        [Required]
        [Display(Name = GameGlobalConstants.DateTimeName)]
        public DateTime DateTime { get; set; }

        [Required]
        [Display(Name = GameGlobalConstants.SportName)]
        public string SportName { get; set; }

        [Required]
        [Display(Name = GameGlobalConstants.CountryName)]
        public string CountryName { get; set; }

        [Required]
        [Display(Name = GameGlobalConstants.CompetitionName)]
        public string CompetitionName { get; set; }

        [Required]
        [Display(Name = GameGlobalConstants.HomeTeamName)]
        public string HomeTeamName { get; set; }

        [Required]
        [Display(Name = GameGlobalConstants.AwayTeamName)]
        public string AwayTeamName { get; set; }

        public IEnumerable<KeyValuePair<string, string>> SportItems { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CountryItems { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CompetitionItems { get; set; }

        public IEnumerable<KeyValuePair<string, string>> TeamItems { get; set; }
    }
}
