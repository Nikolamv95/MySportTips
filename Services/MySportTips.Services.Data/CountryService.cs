namespace MySportTips.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MySportTips.Data.Common.Repositories;
    using MySportTips.Data.Models;

    public class CountryService : ICountryService
    {
        private readonly IDeletableEntityRepository<Country> countryRepository;

        public CountryService(IDeletableEntityRepository<Country> countryRepository)
        {
            this.countryRepository = countryRepository;
        }

        public async Task AddCountryAsync(string name)
        {
            var country = new Country()
            {
                Name = name,
            };

            await this.countryRepository.AddAsync(country);
            await this.countryRepository.SaveChangesAsync();
        }

        public bool IsCountryExist(string name)
        {
            if (!this.countryRepository.All().Any(x => x.Name.Contains(name)))
            {
                return false;
            }

            return true;
        }

        public int GetCountryId(string name)
        {
            if (!this.IsCountryExist(name))
            {
                throw new ArgumentException("This country doesn't exist.");
            }

            var country = this.countryRepository.All().FirstOrDefault(x => x.Name == name);
            return country.Id;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllKeyValuePairs()
        {
            return this.countryRepository
                .All()
                .Select(x => new { x.Id, x.Name })
                .OrderBy(x => x.Name)
                .ToList()
                .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }
    }
}
