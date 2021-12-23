using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DBTask.Core
{
    public class DataBaseInitializer
    {
        private readonly ILogger<DataBaseInitializer> _log;
        private readonly DataBaseContext _context;
        public DataBaseInitializer(ILogger<DataBaseInitializer> log, DataBaseContext context)
        {
            _log = log;
            _context = context;
        }
        private void TryRun(Action action)
        {
            var _success = false;
            var _triesCount = 0;
            while (!_success && _triesCount <= 5)
            {
                try
                {
                    action.Invoke();
                    _success = true;
                }
                catch (Exception ex)
                {
                    _success = false;
                    _log.LogError(ex, ex.Message);
                    _triesCount++;
                }
            }
            if (_triesCount == 6)
            {
                _log.LogError("Максимальное кол-во попыток превышено");
                throw new Exception("Максимальное кол-во попыток превышено");
            }
        }

        public void Initialize()
        {
            _context.Database.Migrate();
            _context.Database.EnsureCreated();
        }
    }
}
