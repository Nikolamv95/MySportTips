namespace MySportTips.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MySportTips.Data.Common.Models;

    public class Status : BaseDeletableModel<int>
    {
        public Status()
        {
            this.Tips = new List<Tip>();
        }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Tip> Tips { get; set; }
    }
}
