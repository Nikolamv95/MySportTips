namespace MySportTips.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MySportTips.Data.Common.Repositories;
    using MySportTips.Data.Models;

    public class SportService : ISportService
    {
        private readonly IDeletableEntityRepository<Sport> sportRepository;

        public SportService(IDeletableEntityRepository<Sport> sportRepository)
        {
            this.sportRepository = sportRepository;
        }

        public async Task AddSportAsync(string name)
        {
            var sport = new Sport()
            {
                Name = name,
            };

            await this.sportRepository.AddAsync(sport);
            await this.sportRepository.SaveChangesAsync();
        }

        public bool IsSportExist(string name)
        {
            if (!this.sportRepository.All().Any(x => x.Name.Contains(name)))
            {
                return false;
            }

            return true;
        }

        public int GetSportId(string name)
        {
            if (!this.IsSportExist(name))
            {
                throw new ArgumentException("This sport doesn't exist.");
            }

            var sport = this.sportRepository.All().FirstOrDefault(x => x.Name == name);
            return sport.Id;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllKeyValuePairs()
        {
            return this.sportRepository
                .All()
                .Select(x => new { x.Id, x.Name })
                .OrderBy(x => x.Name)
                .ToList()
                .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }
    }
}
