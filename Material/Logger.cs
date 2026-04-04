using System.IO;
using System;
using Microsoft.Extensions.Configuration;

namespace BuildExeMaterialServices
{
    public static class Logger
    {
        private static string logFileName = string.Empty;
        private static readonly object _fileLock = new object();
        public static void ErrorLog(string className = "", string methodName = "", Exception exception = null)
        {
            try
            {
                if (exception == null)
                    return;

                if (exception.Message == "Thread was being aborted.")
                    return;

                if (string.IsNullOrEmpty(logFileName))
                {
                    logFileName = GetLogFileName();
                }

                var dirPath = Path.Combine("C:", "ServiceLogs");
                if (!Directory.Exists(dirPath))
                    Directory.CreateDirectory(dirPath);

                var filePath = Path.Combine(dirPath, logFileName + ".log");

                var errMsg = exception.Message?.ToString().Replace("\n", "") ?? string.Empty;

                // Use a file lock and open the file for append with shared read/write so
                // multiple processes can write without exclusive locking issues.
                lock (_fileLock)
                {
                    using (var fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                    {
                        fs.Seek(0, SeekOrigin.End);
                        using (var sw = new StreamWriter(fs))
                        {
                            sw.WriteLine("[" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "]&&[" + className + "]&&[" + methodName + "]&&[" + errMsg + "]");
                        }
                    }
                }
            }
            catch
            {
                // Swallow logging exceptions to avoid affecting application flow when logging fails.
            }
        }

        private static string GetLogFileName()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
            return string.IsNullOrEmpty(config["LogFileName"]) ? "ErrorLog" : config["LogFileName"].Trim().ToString();
        }
    }
}
