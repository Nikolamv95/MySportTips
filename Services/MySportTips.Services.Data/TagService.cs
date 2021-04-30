namespace MySportTips.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using MySportTips.Data.Common.Repositories;
    using MySportTips.Data.Models;

    public class TagService : ITagService
    {
        private readonly IDeletableEntityRepository<Tag> tagRepository;

        public TagService(IDeletableEntityRepository<Tag> tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        public async Task AddTagAsync(string name)
        {
            var tag = new Tag()
            {
                Name = name,
            };

            await this.tagRepository.AddAsync(tag);
            await this.tagRepository.SaveChangesAsync();
        }

        public bool IsTagExist(string name)
        {
            if (!this.tagRepository.All().Any(x => x.Name.Contains(name)))
            {
                return false;
            }

            return true;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllKeyValuePairs()
        {
            return this.tagRepository
                .All()
                .Select(x => new { x.Id, x.Name })
                .OrderBy(x => x.Name)
                .ToList()
                .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }

        public int GetTagId(string name)
        {
            if (!this.IsTagExist(name))
            {
                throw new ArgumentException("This tag doesn't exist.");
            }

            var country = this.tagRepository.All().FirstOrDefault(x => x.Name == name);
            return country.Id;
        }
    }
}
