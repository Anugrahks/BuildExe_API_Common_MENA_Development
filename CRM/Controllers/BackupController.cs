using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace BuildExeServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackupController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private const string StaticKey = "BackupInitializer@4620";

        public BackupController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("RunBackup")]
        [Authorize]
        public IActionResult RunBackup()
        {
            try
            {
                var sqlBackupMasterPath = @"C:\Program Files (x86)\Key Metric Software\SQL Backup Master\SQLBackupMaster.exe";
                var backupJobName = "New Database Backup";

                var startInfo = new ProcessStartInfo
                {
                    FileName = sqlBackupMasterPath,
                    Arguments = $"/jobName \"{backupJobName}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                Task.Run(() =>
                {
                    using (var process = new Process { StartInfo = startInfo })
                    {
                        process.Start();
                        string output = process.StandardOutput.ReadToEnd();
                        string error = process.StandardError.ReadToEnd();
                        process.WaitForExit();

                        if (process.ExitCode != 0)
                        {
                            Console.WriteLine($"Backup failed with error: {error}");
                        }
                        else
                        {
                            Console.WriteLine("Backup completed successfully");
                        }
                    }
                });

                return Ok("Backup process started successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    [HttpPost("DownloadBackup")]
    public IActionResult DownloadBackup([FromBody] BackupRequest request, [FromHeader(Name = "X-Static-Key")] string staticKey)
    {
        if (staticKey != StaticKey)
            return Unauthorized("Invalid static key.");

        if (string.IsNullOrWhiteSpace(request.DatabaseName))
            return BadRequest("Database name is required.");

            var dbName = request.DatabaseName.ToLower();

            request.DatabaseName = dbName switch
            {
                "floreat" => "Floret_Latest",
                "glory" => "Glory_Latest",
                "c-two" => "CTWO",
                "mariyagroup" => "MariaGroup",
                "master" => "MasterBuilder",
                "diamond" => "Diamond_New",
                "new" => "new",
                "mena" => "new",
                _ => dbName
            };

            try
        {
            string connectionString = CreateDynamicConnectionString(request.DatabaseName);
            string backupFilePath = CreateDatabaseBackup(request.DatabaseName, connectionString);

            if (!System.IO.File.Exists(backupFilePath))
                return NotFound("Backup file not found.");

            var fileStream = new FileStream(backupFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            return File(fileStream, "application/octet-stream", Path.GetFileName(backupFilePath));
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Backup failed: {ex.Message}");
        }
    }

        private string CreateDynamicConnectionString(string databaseName)
        {
            string baseConnectionString = _configuration.GetConnectionString("DbConnection");
            var builder = new SqlConnectionStringBuilder(baseConnectionString)
            {
                InitialCatalog = databaseName
            };
            return builder.ConnectionString;
        }


        private string CreateDatabaseBackup(string databaseName, string connectionString)
    {
        string backupDirectory = @"C:\Backups\";
        if (!Directory.Exists(backupDirectory))
            Directory.CreateDirectory(backupDirectory);

        string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
        string backupFileName = $"{databaseName}_Backup_{timestamp}.bak";
        string backupFilePath = Path.Combine(backupDirectory, backupFileName);


        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string backupCommand = $@"
                BACKUP DATABASE [{databaseName}]
                TO DISK = '{backupFilePath}'
                WITH COPY_ONLY, COMPRESSION, FORMAT, INIT,
                NAME = 'Full Backup of {databaseName}'";

            using (SqlCommand command = new SqlCommand(backupCommand, connection))
            {
                command.CommandTimeout = 0; 
                command.ExecuteNonQuery();
            }
        }

        return backupFilePath;
    }

}

public class BackupRequest
{
    public string DatabaseName { get; set; }
}
}
