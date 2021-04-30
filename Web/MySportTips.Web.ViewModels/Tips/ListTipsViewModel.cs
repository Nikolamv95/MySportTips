namespace MySportTips.Web.ViewModels.Tips
{
    using System.Collections.Generic;

    public class ListTipsViewModel
    {
        public ICollection<TipViewModel> Tips { get; set; }
    }
}
