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
        public IActionResult AllTips()
        {
            var tipsViewModel = new ListTipsViewModel()
            {
                Tips = this.tipService.GetAllTips(),
            };

            return this.View(tipsViewModel);
        }

        [HttpGet]
        public IActionResult AllCurrentTips()
        {
            var tipsViewModel = new ListTipsViewModel()
            {
                Tips = this.tipService.GetAllCurrentTips(),
            };

            return this.View(tipsViewModel);
        }

        [HttpGet]
        public IActionResult AllPastTips()
        {
            var tipsViewModel = new ListTipsViewModel()
            {
                Tips = this.tipService.GetAllPastTips(),
            };

            return this.View(tipsViewModel);
        }

        [HttpGet]
        public IActionResult ById(int id)
        {
            var tipViewModel = this.tipService.GetById(id);
            return this.View(tipViewModel);
        }
    }
}
