namespace MySportTips.Web.ViewModels.Games
{
    using System.Collections.Generic;

    using MySportTips.Web.ViewModels.Global;

    public class ListGamesViewModel : PagingViewModel
    {
        public ICollection<GameViewModel> Games { get; set; }
    }
}
