namespace MySportTips.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MySportTips.Data.Common.Models;

    public class Game : BaseDeletableModel<int>
    {
        public Game()
        {
            this.Tips = new HashSet<Tip>();
        }

        [Required]
        public DateTime DateTime { get; set; }

        public int SportId { get; set; }

        public virtual Sport Sport { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public int CompetitionId { get; set; }

        public virtual Competition Competition { get; set; }

        public int HomeTeamId { get; set; }

        public virtual Team HomeTeam { get; set; }

        public int AwayTeamId { get; set; }

        public virtual Team AwayTeam { get; set; }

        public virtual ICollection<Tip> Tips { get; set; }

        public string Result { get; set; }

        public string Statistics { get; set; }
    }
}
