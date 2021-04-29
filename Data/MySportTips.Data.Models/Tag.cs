namespace MySportTips.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MySportTips.Data.Common.Models;

    public class Tag : BaseDeletableModel<int>
    {
        public Tag()
        {
            this.TipTags = new List<TipTag>();
        }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<TipTag> TipTags { get; set; }
    }
}
