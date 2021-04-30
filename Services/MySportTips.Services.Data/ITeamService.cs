namespace MySportTips.Services.Data
{
    using System.Threading.Tasks;

    public interface ITeamService
    {
        public Task AddTeam(string name);

        public bool IsTeamExist(string name);

        public int GetTeamId(string name);
    }
}
