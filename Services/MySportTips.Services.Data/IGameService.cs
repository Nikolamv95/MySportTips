namespace MySportTips.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MySportTips.Web.ViewModels.Games;

    public interface IGameService
    {
        public Task CreateGameAsync(AddGameInputModel gameInputModel);

        public AddGameInputModel MapAllGameItems();

        public ICollection<GameViewModel> GetAllGamesOrderByDate(int page, int itemsPerPage = 10);

        public GameViewModel GetById(int id);

        public bool IsGameExist(int id);

        public EditGameInputModel MapEditGameModel(int id);

        public Task EditGameAsync(EditGameInputModel gameInputModel);

        public int GetCount();
    }
}
