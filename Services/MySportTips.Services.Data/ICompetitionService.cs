namespace MySportTips.Services.Data
{
    using System.Threading.Tasks;

    public interface ICompetitionService
    {
        public Task AddCompetition(string name);

        public bool IsCompetitionExist(string name);

        public int GetCompetitionId(string name);
    }
}
