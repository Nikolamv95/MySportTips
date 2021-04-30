namespace MySportTips.Web.ViewModels.Games
{
    using System;

    public class GameViewModel
    {
        public int GameId { get; set; }

        public DateTime DateTime { get; set; }

        public string SportName { get; set; }

        public string CountryName { get; set; }

        public string CompetitionName { get; set; }

        public string HomeTeamName { get; set; }

        public string AwayTeamName { get; set; }

        public string Result { get; set; }

        public string Statistic { get; set; }
    }
}
