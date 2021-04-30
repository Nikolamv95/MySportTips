namespace MySportTips.Services.Data
{
    using System.Threading.Tasks;

    public interface ISportService
    {
        public Task AddSport(string name);

        public bool IsSportExist(string name);

        public int GetSportId(string name);
    }
}
