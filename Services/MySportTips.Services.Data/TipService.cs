using System.Linq.Expressions;

namespace MySportTips.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using MySportTips.Data.Common.Repositories;
    using MySportTips.Data.Models;
    using MySportTips.Web.ViewModels.Tips;

    public class TipService : ITipService
    {
        private static string currentTips = "Currently playing";
        private static string pastTips = "Finished";
        private readonly string[] allowedExtensions = new[] { "jpg", "png" };
        private readonly IDeletableEntityRepository<Tip> tipRepository;
        private readonly IStatusService statusService;
        private readonly ITimePeriodService timePeriodService;
        private readonly ITagService tagService;
        private readonly IDeletableEntityRepository<TipTag> tipTagRepository;
        private readonly IGameService gameService;

        public TipService(IDeletableEntityRepository<Tip> tipRepository, IStatusService statusService, ITimePeriodService timePeriodService, ITagService tagService, IDeletableEntityRepository<TipTag> tipTagRepository, IGameService gameService)
        {
            this.tipRepository = tipRepository;
            this.statusService = statusService;
            this.timePeriodService = timePeriodService;
            this.tagService = tagService;
            this.tipTagRepository = tipTagRepository;
            this.gameService = gameService;
        }

        public async Task CreateTipAsync(AddTipInputModel tipInputModel, string imagePath)
        {
            if (!this.gameService.IsGameExist(tipInputModel.GameId))
            {
                throw new ArgumentException("This game doesn't exist");
            }

            if (!this.statusService.IsStatusExist(tipInputModel.StatusName))
            {
                await this.statusService.AddStatusAsync(tipInputModel.StatusName);
            }

            if (!this.timePeriodService.IsTimePeriodExist(tipInputModel.TimePeriod))
            {
                await this.timePeriodService.AddTimePeriodAsync(tipInputModel.TimePeriod);
            }

            var tip = new Tip()
            {
                GameId = tipInputModel.GameId,
                Selection = tipInputModel.Selection,
                Description = tipInputModel.Description,
                Odd = tipInputModel.Odd,
                StatusId = this.statusService.GetStatusId(tipInputModel.StatusName),
                TimePeriodId = this.timePeriodService.GetTimePeriodId(tipInputModel.TimePeriod),
            };

            if (!this.tagService.IsTagExist(tipInputModel.TagName))
            {
                await this.tagService.AddTagAsync(tipInputModel.TagName);
            }

            await this.tipRepository.AddAsync(tip);
            await this.tipRepository.SaveChangesAsync();

            var tipTag = new TipTag()
            {
                TagId = this.tagService.GetTagId(tipInputModel.TagName),
                TipId = this.GetTipIdByGame(tipInputModel.GameId),
            };

            await this.tipTagRepository.AddAsync(tipTag);
            await this.tipTagRepository.SaveChangesAsync();

        }

        public ICollection<TipViewModel> GetAllTips()
        {
            return this.tipRepository.All().Select(MapAllTips()).ToList();
        }

        public ICollection<TipViewModel> GetAllCurrentTips()
        {
            return this.tipRepository.All().Where(x => x.TimePeriod.Name == currentTips).Select(MapAllTips()).ToList();
        }

        public ICollection<TipViewModel> GetAllPastTips()
        {
            return this.tipRepository.All().Where(x => x.TimePeriod.Name == pastTips).Select(MapAllTips()).ToList();
        }

        public TipViewModel GetById(int id)
        {
            return this.tipRepository.All().Where(x => x.Id == id).Select(x => new TipViewModel
            {
                TipId = x.Id,
                GameId = x.Game.Id,
                DateTime = x.Game.DateTime,
                SportName = x.Game.Sport.Name,
                CountryName = x.Game.Country.Name,
                CompetitionName = x.Game.Competition.Name,
                HomeTeamName = x.Game.HomeTeam.Name,
                AwayTeamName = x.Game.AwayTeam.Name,
                Selection = x.Selection,
                Description = x.Description,
                Odd = x.Odd,
                StatusName = x.Status.Name,
                TimePeriod = x.TimePeriod.Name,
                Tag = x.TipTags.Where(t => t.TipId == id).Select(x => x.Tag.Name).FirstOrDefault(),
            }).FirstOrDefault();
        }

        public AddTipInputModel MapAllTipItems(int gameId)
        {
            return new AddTipInputModel()
            {
                GameId = gameId,
                TagItems = this.tagService.GetAllKeyValuePairs(),
                SelectionItems = this.GetAllKeyValuePairsSelection(),
            };
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllKeyValuePairsSelection()
        {
            return this.tipRepository
                    .All()
                    .Select(x => new { x.Id, x.Selection })
                    .OrderBy(x => x.Selection)
                    .ToList()
                    .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Selection));
        }

        private int GetTipIdByGame(int id)
        {
            var tip = this.tipRepository.All().FirstOrDefault(x => x.GameId == id);
            return tip.Id;
        }

        private static Expression<Func<Tip, TipViewModel>> MapAllTips()
        {
            return x => new TipViewModel
            {
                TipId = x.Id,
                GameId = x.Game.Id,
                DateTime = x.Game.DateTime,
                SportName = x.Game.Sport.Name,
                CountryName = x.Game.Country.Name,
                CompetitionName = x.Game.Competition.Name,
                HomeTeamName = x.Game.HomeTeam.Name,
                AwayTeamName = x.Game.AwayTeam.Name,
                Selection = x.Selection,
                Description = x.Description,
                Odd = x.Odd,
                StatusName = x.Status.Name,
                TimePeriod = x.TimePeriod.Name,
            };
        }
    }
}
