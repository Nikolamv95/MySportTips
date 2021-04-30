namespace MySportTips.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MySportTips.Data.Common.Repositories;
    using MySportTips.Data.Models;

    public class CompetitionService : ICompetitionService
    {
        private readonly IDeletableEntityRepository<Competition> competitionRepository;

        public CompetitionService(IDeletableEntityRepository<Competition> competitionRepository)
        {
            this.competitionRepository = competitionRepository;
        }

        public async Task AddCompetition(string name)
        {
            if (name == null)
            {
                throw new ArgumentException("Competition name is empty");
            }

            var competition = new Competition()
            {
                Name = name,
            };

            await this.competitionRepository.AddAsync(competition);
            await this.competitionRepository.SaveChangesAsync();
        }

        public bool IsCompetitionExist(string name)
        {
            if (!this.competitionRepository.All().Any(x => x.Name.Contains(name)))
            {
                return false;
            }

            return true;
        }

        public int GetCompetitionId(string name)
        {
            var country = this.competitionRepository.All().FirstOrDefault(x => x.Name == name);
            return country.Id;
        }
    }
}
