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
            return this.View();
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> AddGame(AddGameInputModel gameInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            try
            {
                await this.gameService.CreateGame(gameInputModel);
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
            return this.View();
        }
    }
}
