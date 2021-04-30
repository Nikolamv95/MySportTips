namespace MySportTips.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using MySportTips.Data.Common.Repositories;
    using MySportTips.Data.Models;

    public class TeamService : ITeamService
    {
        private readonly IDeletableEntityRepository<Team> teamRepository;

        public TeamService(IDeletableEntityRepository<Team> teamRepository)
        {
            this.teamRepository = teamRepository;
        }

        public async Task AddTeam(string name)
        {
            var team = new Team()
            {
                Name = name,
            };

            await this.teamRepository.AddAsync(team);
            await this.teamRepository.SaveChangesAsync();
        }

        public bool IsTeamExist(string name)
        {
            if (!this.teamRepository.All().Any(x => x.Name.Contains(name)))
            {
                return false;
            }

            return true;
        }

        public int GetTeamId(string name)
        {
            var team = this.teamRepository.All().FirstOrDefault(x => x.Name == name);
            return team.Id;
        }
    }
}
