using System.IO;
using System;
using Microsoft.Extensions.Configuration;

namespace BuildExeServices
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
                    StreamReader sReader;
                    StreamWriter sWriter;
                    if (string.IsNullOrEmpty(logFileName))
                    {
                        logFileName = GetLogFileName();
                    }
                    string filePath = string.Format($"C:/ServiceLogs/" + logFileName + ".log");
                    string readText = string.Empty;


                    if (File.Exists(filePath))
                    {
                        sReader = File.OpenText(filePath);
                        readText = sReader.ReadToEnd();
                        sReader.Close();
                    }
                    else
                    {
                        sWriter = File.CreateText(filePath);
                        sWriter.Close();
                    }

                    sWriter = new StreamWriter(filePath);
                    if (!string.IsNullOrEmpty(readText))
                    {
                        readText = readText.Remove(readText.Length - 2);
                        sWriter.WriteLine(readText);
                    }

                    string errMsg = exception.Message.ToString().Replace("\n", "");

                    sWriter.WriteLine("[" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + "]&&[" + className + "]&&[" + methodName + "]&&[" + errMsg + "]");
                    sWriter.Close();
                }
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
