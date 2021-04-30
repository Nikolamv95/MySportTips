namespace MySportTips.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MySportTips.Data.Common.Repositories;
    using MySportTips.Data.Models;

    public class TimePeriodService : ITimePeriodService
    {
        private readonly IDeletableEntityRepository<TimePeriod> timePeriodRepository;

        public TimePeriodService(IDeletableEntityRepository<TimePeriod> timePeriodRepository)
        {
            this.timePeriodRepository = timePeriodRepository;
        }

        public async Task AddTimePeriodAsync(string name)
        {
            var tag = new TimePeriod()
            {
                Name = name,
            };

            await this.timePeriodRepository.AddAsync(tag);
            await this.timePeriodRepository.SaveChangesAsync();
        }

        public bool IsTimePeriodExist(string name)
        {
            if (!this.timePeriodRepository.All().Any(x => x.Name.Contains(name)))
            {
                return false;
            }

            return true;
        }

        public int GetTimePeriodId(string name)
        {
            if (!this.IsTimePeriodExist(name))
            {
                throw new ArgumentException("This timePeriod doesn't exist.");
            }

            var country = this.timePeriodRepository.All().FirstOrDefault(x => x.Name == name);
            return country.Id;
        }
    }
}
