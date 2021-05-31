namespace MySportTips.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MySportTips.Web.ViewModels.Tips;

    public interface ITipService
    {
        public Task CreateTipAsync(AddTipInputModel tipInputModel, string imagePath);

        public ICollection<TipViewModel> GetAllTips(int page, int itemsPerPage);

        public ICollection<TipViewModel> GetAllCurrentPastTips(string timePeriod, int page, int itemsPerPage);

        public TipViewModel GetById(int id);

        public AddTipInputModel MapAllTipItems(int gameId);

        public IEnumerable<string> GetAllKeyValuePairsSelection();

        public EditTipInputModel MapEditTipModel(int id);

        public Task EditTipAsync(EditTipInputModel editTipInput);

        public int GetCount();
    }
}
