namespace MySportTips.Web.ViewModels.Games
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using MySportTips.Common;

    public class AddGameInputModel
    {
        [Required]
        [Display(Name = GameGlobalConstants.DateTimeName)]
        public DateTime DateTime { get; set; }

        [Required]
        [Display(Name = GameGlobalConstants.SportName)]
        [MinLength(GameGlobalConstants.SportNameLength)]
        public string SportName { get; set; }

        [Required]
        [Display(Name = GameGlobalConstants.CountryName)]
        [MinLength(GameGlobalConstants.CountryNameLength)]
        public string CountryName { get; set; }

        [Required]
        [Display(Name = GameGlobalConstants.CompetitionName)]
        [MinLength(GameGlobalConstants.CompetitionNameLength)]
        public string CompetitionName { get; set; }

        [Required]
        [Display(Name = GameGlobalConstants.HomeTeamName)]
        [MinLength(GameGlobalConstants.HomeTeamNameLength)]
        public string HomeTeamName { get; set; }

        [Required]
        [Display(Name = GameGlobalConstants.AwayTeamName)]
        [MinLength(GameGlobalConstants.AwayTeamNameLength)]
        public string AwayTeamName { get; set; }
    }
}
