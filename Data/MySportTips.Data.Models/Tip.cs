namespace MySportTips.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MySportTips.Data.Common.Models;

    public class Tip : BaseDeletableModel<int>
    {
        public Tip()
        {
            this.TipTags = new HashSet<TipTag>();
        }

        public int GameId { get; set; }

        public virtual Game Game { get; set; }

        [Required]
        public string Selection { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Odd { get; set; }

        public int StatusId { get; set; }

        public virtual Status Status { get; set; }

        public int TimePeriodId { get; set; }

        public virtual TimePeriod TimePeriod { get; set; }

        public string ImageId { get; set; }

        public virtual Image Image { get; set; }

        public virtual ICollection<TipTag> TipTags { get; set; }
    }
}
