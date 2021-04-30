namespace MySportTips.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MySportTips.Data.Common.Repositories;
    using MySportTips.Data.Models;
    using MySportTips.Web.ViewModels.Games;

    public class GameService : IGameService
    {
        private readonly IDeletableEntityRepository<Game> gameRepository;
        private readonly ISportService sportService;
        private readonly ICountryService countryService;
        private readonly ICompetitionService competitionService;
        private readonly ITeamService teamService;

        public GameService(IDeletableEntityRepository<Game> gameRepository, ISportService sportService, ICountryService countryService, ICompetitionService competitionService, ITeamService teamService)
        {
            this.gameRepository = gameRepository;
            this.sportService = sportService;
            this.countryService = countryService;
            this.competitionService = competitionService;
            this.teamService = teamService;
        }

        public async Task CreateGameAsync(AddGameInputModel gameInputModel)
        {
            if (!this.sportService.IsSportExist(gameInputModel.SportName))
            {
                await this.sportService.AddSportAsync(gameInputModel.SportName);
            }

            if (!this.countryService.IsCountryExist(gameInputModel.CountryName))
            {
                await this.countryService.AddCountryAsync(gameInputModel.CountryName);
            }

            if (!this.competitionService.IsCompetitionExist(gameInputModel.CompetitionName))
            {
                await this.competitionService.AddCompetitionAsync(gameInputModel.CompetitionName);
            }

            if (!this.teamService.IsTeamExist(gameInputModel.HomeTeamName))
            {
                await this.teamService.AddTeamAsync(gameInputModel.HomeTeamName);
            }

            if (!this.teamService.IsTeamExist(gameInputModel.AwayTeamName))
            {
                await this.teamService.AddTeamAsync(gameInputModel.AwayTeamName);
            }

            var game = new Game()
            {
                DateTime = gameInputModel.DateTime,
                SportId = this.sportService.GetSportId(gameInputModel.SportName),
                CountryId = this.countryService.GetCountryId(gameInputModel.CountryName),
                CompetitionId = this.competitionService.GetCompetitionId(gameInputModel.CompetitionName),
                HomeTeamId = this.teamService.GetTeamId(gameInputModel.HomeTeamName),
                AwayTeamId = this.teamService.GetTeamId(gameInputModel.AwayTeamName),
            };

            await this.gameRepository.AddAsync(game);
            await this.gameRepository.SaveChangesAsync();
        }

        public AddGameInputModel MapAllGameItems()
        {
            return new AddGameInputModel()
            {
                SportItems = this.sportService.GetAllKeyValuePairs(),
                CountryItems = this.countryService.GetAllKeyValuePairs(),
                CompetitionItems = this.competitionService.GetAllKeyValuePairs(),
                TeamItems = this.teamService.GetAllKeyValuePairs(),
            };
        }

        public ICollection<GameViewModel> GetAllGamesOrderByAddDate()
        {
            return this.gameRepository
                .All()
                .OrderByDescending(x => x.CreatedOn)
                .Select(x => new GameViewModel()
                {
                    GameId = x.Id,
                    DateTime = x.DateTime,
                    SportName = x.Sport.Name,
                    CountryName = x.Country.Name,
                    CompetitionName = x.Competition.Name,
                    HomeTeamName = x.HomeTeam.Name,
                    AwayTeamName = x.AwayTeam.Name,
                    Result = x.Result,
                    Statistic = x.Statistics,
                })
                .ToList();
        }

        public GameViewModel GetById(int id)
        {
            return this.gameRepository.All()
                 .Where(x => x.Id == id)
                 .Select(x => new GameViewModel()
                 {
                     GameId = x.Id,
                     DateTime = x.DateTime,
                     SportName = x.Sport.Name,
                     CountryName = x.Country.Name,
                     CompetitionName = x.Competition.Name,
                     HomeTeamName = x.HomeTeam.Name,
                     AwayTeamName = x.AwayTeam.Name,
                     Result = x.Result,
                     Statistic = x.Statistics,
                 }).FirstOrDefault();
        }

        public bool IsGameExist(int id)
        {
            if (!this.gameRepository.All().Any(x => x.Id == id))
            {
                return false;
            }

            return true;
        }
    }
}
