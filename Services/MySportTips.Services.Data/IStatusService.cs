using MySportTips.Data.Models;

namespace MySportTips.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IStatusService
    {
        public Task AddStatusAsync(string name);

        public bool IsStatusExist(string name);

        public int GetStatusId(string name);

        public Status GetStatus(string name);
    }
}
