namespace MySportTips.Services.Data
{
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

        public async Task AddCountry(string name)
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
            var country = this.countryRepository.All().FirstOrDefault(x => x.Name == name);
            return country.Id;
        }
    }
}
