﻿namespace MySportTips.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITimePeriodService
    {
        public Task AddTimePeriodAsync(string name);

        public bool IsTimePeriodExist(string name);

        public int GetTimePeriodId(string name);
    }
}
