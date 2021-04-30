namespace MySportTips.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using MySportTips.Data.Common.Repositories;
    using MySportTips.Data.Models;

    public class StatusService : IStatusService
    {
        private readonly IDeletableEntityRepository<Status> statusRepository;

        public StatusService(IDeletableEntityRepository<Status> statusRepository)
        {
            this.statusRepository = statusRepository;
        }

        public async Task AddStatusAsync(string name)
        {
            var status = new Status()
            {
                Name = name,
            };

            await this.statusRepository.AddAsync(status);
            await this.statusRepository.SaveChangesAsync();
        }

        public bool IsStatusExist(string name)
        {
            if (!this.statusRepository.All().Any(x => x.Name.Contains(name)))
            {
                return false;
            }

            return true;
        }

        public int GetStatusId(string name)
        {
            if (!this.IsStatusExist(name))
            {
                throw new ArgumentException("This status doesn't exist.");
            }

            var country = this.statusRepository.All().FirstOrDefault(x => x.Name == name);
            return country.Id;
        }
    }
}
