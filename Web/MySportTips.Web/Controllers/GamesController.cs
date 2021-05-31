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
        [ValidateAntiForgeryToken]
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
            catch (ArgumentException ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                var gameViewItems = this.gameService.MapAllGameItems();
                return this.View(gameViewItems);
            }

            this.TempData["Message"] = "Game was added successfully.";
            return this.RedirectToAction(nameof(this.AllGames));
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet]
        public IActionResult EditGame(int id)
        {
            var editGameInputModel = this.gameService.MapEditGameModel(id);
            return this.View(editGameInputModel);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> EditGame(EditGameInputModel editGameInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            try
            {
                await this.gameService.EditGameAsync(editGameInputModel);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View();
            }

            this.TempData["Message"] = "The game was edited successfully.";
            return this.RedirectToAction(nameof(this.AllGames));
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," + GlobalConstants.MemberRoleName)]
        public IActionResult AllGames(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int itemsPerPage = 8;

            var gamesViewModel = new ListGamesViewModel()
            {
                PageNumber = id,
                Games = this.gameService.GetAllGamesOrderByDate(id, itemsPerPage),
                EventsCount = this.gameService.GetCount(),
                ItemsPerPage = itemsPerPage,
            };

            return this.View(gamesViewModel);
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," + GlobalConstants.MemberRoleName)]
        public IActionResult ById(int id)
        {
            var gameViewModel = this.gameService.GetById(id);
            return this.View(gameViewModel);
        }
    }
}
