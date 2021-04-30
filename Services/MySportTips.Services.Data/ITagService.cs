namespace MySportTips.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITagService
    {
        public Task AddTagAsync(string name);

        public bool IsTagExist(string name);

        IEnumerable<KeyValuePair<string, string>> GetAllKeyValuePairs();

        public int GetTagId(string name);
    }
}
