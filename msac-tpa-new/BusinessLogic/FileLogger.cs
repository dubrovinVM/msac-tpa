using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace msac_tpa_new.BusinessLogic
{
    public class FileLogger : ILogger
    {
        private string filePath;
        private object _lock = new object();

        public FileLogger(string path)
        {
            filePath = path;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            //return logLevel == LogLevel.Trace;
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter != null)
            {
                lock (_lock)
                {
                    try
                    {
                        File.AppendAllText(filePath, formatter(state, exception) + Environment.NewLine);
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }
            }
        }
    }
}