using Microsoft.AspNetCore.Authorization;
using MySportTips.Common;
using MySportTips.Data.Common.Repositories;
using MySportTips.Data.Models;
using MySportTips.Services.Data;
using MySportTips.Web.ViewModels.Tips;

namespace MySportTips.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class TipsController : BaseController
    {
        private readonly ITipService tipService;

        public TipsController(ITipService tipService)
        {
            this.tipService = tipService;
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet]
        public async Task<IActionResult> AddTip()
        {
            return this.View();
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> AddTip(AddTipInputModel tipInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }


            return this.View();
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet]
        public async Task<IActionResult> AllTips()
        {
            return this.View();
        }
    }
}
