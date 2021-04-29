namespace MySportTips.Data.Models
{
    using System.Collections.Generic;

    using MySportTips.Data.Common.Models;

    public class Image : BaseDeletableModel<string>
    {
        public Image()
        {
            this.Tips = new List<Tip>();
        }

        public string Extension { get; set; }

        public string Url { get; set; }

        public virtual ICollection<Tip> Tips { get; set; }
    }
}
