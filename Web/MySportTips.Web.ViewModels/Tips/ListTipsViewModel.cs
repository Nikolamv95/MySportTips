namespace MySportTips.Web.ViewModels.Tips
{
    using System.Collections.Generic;

    using MySportTips.Web.ViewModels.Global;

    public class ListTipsViewModel : PagingViewModel
    {
        public ICollection<TipViewModel> Tips { get; set; }
    }
}
