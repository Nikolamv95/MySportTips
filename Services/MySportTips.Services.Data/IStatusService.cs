namespace MySportTips.Services.Data
{
    using System.Threading.Tasks;

    public interface IStatusService
    {
        public Task AddStatusAsync(string name);

        public bool IsStatusExist(string name);

        public int GetStatusId(string name);
    }
}
