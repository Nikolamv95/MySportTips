namespace MySportTips.Services.Data
{
    using System;
    using System.Collections.Generic;
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
            if (!this.IsCompetitionExist(name))
            {
                throw new ArgumentException("This competition doesn't exist.");
            }

            var country = this.competitionRepository.All().FirstOrDefault(x => x.Name == name);
            return country.Id;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllKeyValuePairs()
        {
            return this.competitionRepository
                .All()
                .Select(x => new { x.Id, x.Name })
                .OrderBy(x => x.Name)
                .ToList()
                .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }
    }
}
