namespace MySportTips.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MySportTips.Web.ViewModels.Tips;

    public interface ITipService
    {
        public Task CreateTipAsync(AddTipInputModel tipInputModel, string imagePath);

        public ICollection<TipViewModel> GetAllTips();

        public ICollection<TipViewModel> GetAllCurrentTips();

        public ICollection<TipViewModel> GetAllPastTips();

        public TipViewModel GetById(int id);

        public AddTipInputModel MapAllTipItems(int gameId);

        public IEnumerable<KeyValuePair<string, string>> GetAllKeyValuePairsSelection();

        public EditTipInputModel MapEditTipModel(int id);

        public Task EditTipAsync(EditTipInputModel editTipInput);
    }
}
