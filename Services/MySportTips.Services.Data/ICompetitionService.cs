namespace MySportTips.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICompetitionService
    {
        public Task AddCompetition(string name);

        public bool IsCompetitionExist(string name);

        public int GetCompetitionId(string name);

        IEnumerable<KeyValuePair<string, string>> GetAllKeyValuePairs();
    }
}
