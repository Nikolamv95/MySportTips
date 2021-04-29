namespace MySportTips.Data.Models
{
    using MySportTips.Data.Common.Models;

    public class TipTag : BaseDeletableModel<int>
    {
        public int TipId { get; set; }

        public virtual Tip Tip { get; set; }

        public int TagId { get; set; }

        public virtual Tag Tag { get; set; }
    }
}
