namespace MySportTips.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITeamService
    {
        public Task AddTeamAsync(string name);

        public bool IsTeamExist(string name);

        public int GetTeamId(string name);

        IEnumerable<KeyValuePair<string, string>> GetAllKeyValuePairs();
    }
}
