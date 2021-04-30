namespace MySportTips.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MySportTips.Web.ViewModels.Games;

    public interface IGameService
    {
        public Task CreateGameAsync(AddGameInputModel gameInputModel);

        public AddGameInputModel MapAllGameItems();

        public ICollection<GameViewModel> GetAllGamesOrderByAddDate();

        public GameViewModel GetById(int id);

        public bool IsGameExist(int id);
    }
}
