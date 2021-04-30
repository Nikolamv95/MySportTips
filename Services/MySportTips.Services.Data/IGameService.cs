namespace MySportTips.Services.Data
{
    using System.Threading.Tasks;

    using MySportTips.Web.ViewModels.Games;

    public interface IGameService
    {
        public Task CreateGame(AddGameInputModel gameInputModel);

        public AddGameInputModel MapAllGameItems();
    }
}
