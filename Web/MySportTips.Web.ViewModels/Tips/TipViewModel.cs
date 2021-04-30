namespace MySportTips.Web.ViewModels.Tips
{
    using System;
    using System.Collections.Generic;

    public class TipViewModel
    {
        public int GameId { get; set; }

        public int TipId { get; set; }

        public DateTime DateTime { get; set; }

        public string SportName { get; set; }

        public string CountryName { get; set; }

        public string CompetitionName { get; set; }

        public string HomeTeamName { get; set; }

        public string AwayTeamName { get; set; }

        public string Selection { get; set; }

        public string Description { get; set; }

        public double Odd { get; set; }

        public string StatusName { get; set; }

        public string TimePeriod { get; set; }

        public string Tag { get; set; }
    }
}
