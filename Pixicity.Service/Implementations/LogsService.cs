using Pixicity.Data;
using Pixicity.Data.Models.Logs;
using Pixicity.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pixicity.Service.Implementations
{
    public class LogsService : ILogsService
    {
        private readonly PixicityDbContext _dbContext;

        public LogsService(PixicityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public long CreateSystemLogs(SystemLogs model)
        {
            try
            {
                //if (!string.IsNullOrEmpty(model.Code))
                //    model.Code = model.Code.Trim().ToUpper();

                //_dbContext.SystemLogs.Add(model);
                //_dbContext.SaveChanges();

                //return model.Id;

                return 0;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public long SaveMonitor(Monitor monitor)
        {
            try
            {
                _dbContext.Monitor.Add(monitor);
                _dbContext.SaveChanges();

                return monitor.Id;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
