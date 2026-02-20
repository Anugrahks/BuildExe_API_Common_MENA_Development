using System.IO;
using System;
using Microsoft.Extensions.Configuration;

namespace BuildExeHR
{
    public static class Logger
    {
        private static string logFileName = string.Empty;

        public static void ErrorLog(string className = "", string methodName = "", Exception exception = null)
        {
            if (exception != null)
            {
                if (exception.Message != "Thread was being aborted.")
                {
                    WriteLog(className, methodName, "ERROR", exception.Message);
                }
            }
        }

        public static void InfoLog(string className = "", string methodName = "", string message = "")
        {
            if (!string.IsNullOrEmpty(message))
            {
                WriteLog(className, methodName, "INFO", message);
            }
        }

        private static void WriteLog(string className, string methodName, string logType, string message)
        {
            if (string.IsNullOrEmpty(logFileName))
            {
                logFileName = GetLogFileName();
            }

            string filePath = $"C:/ServiceLogs/{logFileName}.log";
            string logEntry = $"[{DateTime.Now:dd-MM-yyyy HH:mm:ss}]&&[{className}]&&[{methodName}]&&[{logType}]&&[{message}]";

            try
            {
                // Ensure directory exists
                string directoryPath = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                using (StreamWriter sWriter = File.AppendText(filePath))
                {
                    sWriter.WriteLine(logEntry);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Logging failed: " + ex.Message);
            }
        }

        private static string GetLogFileName()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
            return string.IsNullOrEmpty(config["LogFileName"]) ? "ErrorLog" : config["LogFileName"].Trim();
        }
    }
}
