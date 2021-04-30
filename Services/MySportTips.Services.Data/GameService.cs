namespace MySportTips.Services.Data
{
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

        public async Task CreateGame(AddGameInputModel gameInputModel)
        {
            if (!this.sportService.IsSportExist(gameInputModel.SportName))
            {
                await this.sportService.AddSport(gameInputModel.SportName);
            }

            if (!this.countryService.IsCountryExist(gameInputModel.CountryName))
            {
                await this.countryService.AddCountry(gameInputModel.CountryName);
            }

            if (!this.competitionService.IsCompetitionExist(gameInputModel.CompetitionName))
            {
                await this.competitionService.AddCompetition(gameInputModel.CompetitionName);
            }

            if (!this.teamService.IsTeamExist(gameInputModel.HomeTeamName))
            {
                await this.teamService.AddTeam(gameInputModel.HomeTeamName);
            }

            if (!this.teamService.IsTeamExist(gameInputModel.AwayTeamName))
            {
                await this.teamService.AddTeam(gameInputModel.AwayTeamName);
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
    }
}
