namespace MySportTips.Services.Data
{
    using System.Threading.Tasks;

    public interface ICountryService
    {
        public Task AddCountry(string name);

        public bool IsCountryExist(string name);

        public int GetCountryId(string name);
    }
}
