namespace MySportTips.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using MySportTips.Common;
    using MySportTips.Services.Data;
    using MySportTips.Web.ViewModels.Tips;

    public class TipsController : BaseController
    {
        private const int ItemsPerPage = 10;
        private readonly ITipService tipService;
        private readonly IWebHostEnvironment environment;

        public TipsController(ITipService tipService, IWebHostEnvironment environment)
        {
            this.tipService = tipService;
            this.environment = environment;
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet]
        public IActionResult AddTip(int gameId)
        {
            var tipView = this.tipService.MapAllTipItems(gameId);
            return this.View(tipView);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddTip(AddTipInputModel tipInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                var tipViewItems = this.tipService.MapAllTipItems(tipInputModel.GameId);
                return this.View(tipViewItems);
            }

            try
            {
                await this.tipService.CreateTipAsync(tipInputModel, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                var tipViewItems = this.tipService.MapAllTipItems(tipInputModel.GameId);
                return this.View(tipViewItems);
            }

            this.TempData["Message"] = "The tips was added successfully.";
            return this.RedirectToAction(nameof(this.AllTips));
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet]
        public IActionResult EditTip(int id)
        {
            var editTipInputModel = this.tipService.MapEditTipModel(id);
            return this.View(editTipInputModel);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> EditTip(EditTipInputModel editTipInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            try
            {
                await this.tipService.EditTipAsync(editTipInputModel);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View();
            }

            this.TempData["Message"] = "The tips was edited successfully.";
            return this.RedirectToAction(nameof(this.AllTips));
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet]
        public IActionResult AllTips(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var tipsViewModel = new ListTipsViewModel()
            {
                PageNumber = id,
                Tips = this.tipService.GetAllTips(id, ItemsPerPage),
                EventsCount = this.tipService.GetCount(),
                ItemsPerPage = ItemsPerPage,
            };

            return this.View(tipsViewModel);
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," + GlobalConstants.MemberRoleName)]
        public IActionResult AllCurrentTips(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const string timePeriod = GameGlobalConstants.PeriodCurrentlyPlaying;
            var tipsViewModel = new ListTipsViewModel()
            {
                PageNumber = id,
                Tips = this.tipService.GetAllCurrentPastTips(timePeriod, id, ItemsPerPage),
                EventsCount = this.tipService.GetCount(),
                ItemsPerPage = ItemsPerPage,
            };

            return this.View(tipsViewModel);
        }

        [HttpGet]
        public IActionResult AllPastTips(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const string timePeriod = GameGlobalConstants.PeriodFinished;
            var tipsViewModel = new ListTipsViewModel()
            {
                PageNumber = id,
                Tips = this.tipService.GetAllCurrentPastTips(timePeriod, id, ItemsPerPage),
                EventsCount = this.tipService.GetCount(),
                ItemsPerPage = ItemsPerPage,
            };

            return this.View(tipsViewModel);
        }

        [HttpGet]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName + "," + GlobalConstants.MemberRoleName)]
        public IActionResult ById(int id)
        {
            var tipViewModel = this.tipService.GetById(id);
            return this.View(tipViewModel);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> DeleteTip(int id)
        {
            if (!this.ModelState.IsValid)
            {
                this.RedirectToAction(nameof(this.AllTips));
            }

            try
            {
                await this.tipService.Delete(id);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                this.RedirectToAction(nameof(this.AllTips));
            }

            this.TempData["Message"] = "The tips was deleted successfully.";
            return this.RedirectToAction(nameof(this.AllTips));
        }


    }
}
