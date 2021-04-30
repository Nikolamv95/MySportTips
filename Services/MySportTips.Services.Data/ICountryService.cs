namespace MySportTips.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICountryService
    {
        public Task AddCountryAsync(string name);

        public bool IsCountryExist(string name);

        public int GetCountryId(string name);

        IEnumerable<KeyValuePair<string, string>> GetAllKeyValuePairs();
    }
}
