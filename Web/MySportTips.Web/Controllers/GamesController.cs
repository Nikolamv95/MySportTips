namespace MySportTips.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MySportTips.Common;
    using MySportTips.Services.Data;
    using MySportTips.Web.ViewModels.Games;

    public class GamesController : BaseController
    {
        private readonly IGameService gameService;

        public GamesController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet]
        public IActionResult AddGame()
        {
            var gameViewItems = this.gameService.MapAllGameItems();
            return this.View(gameViewItems);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> AddGame(AddGameInputModel gameInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                var gameViewItems = this.gameService.MapAllGameItems();
                return this.View(gameViewItems);
            }

            try
            {
                await this.gameService.CreateGameAsync(gameInputModel);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View();
            }

            return this.Redirect("/Home/Index");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet]
        public IActionResult AllGames()
        {
            var gamesViewModel = new ListGamesViewModel()
            {
                Games = this.gameService.GetAllGamesOrderByAddDate(),
            };

            return this.View(gamesViewModel);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet]
        public IActionResult ById(int id)
        {
            var gameViewModel = this.gameService.GetById(id);
            return this.View(gameViewModel);
        }
    }
}
