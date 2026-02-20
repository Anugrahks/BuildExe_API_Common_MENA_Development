using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Configuration;

namespace BuildExeServices.Library
{
    public interface IMdHashValidator
    {
        Task<bool> ValidateMdHashAsync(string requestMdHash, int identityId);
    }

    public class MdHashValidator : IMdHashValidator
    {
        private readonly string _connectionString;

        public MdHashValidator(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbConnection"); // Adjust connection string as needed
        }

        public async Task<bool> ValidateMdHashAsync(string requestMdHash, int identityId)
        {
            bool isValid = false;

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("Stpro_ValidateMD5Hash", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                // Add input parameter
                command.Parameters.Add(new SqlParameter("@IdentityId", System.Data.SqlDbType.Int)
                {
                    Value = identityId
                });

                // Add output parameter
                var outputHashParam = new SqlParameter("@OutputHash", System.Data.SqlDbType.NVarChar, 32)
                {
                    Direction = System.Data.ParameterDirection.Output
                };
                command.Parameters.Add(outputHashParam);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                // Get the output hash value
                string storedMdHash = outputHashParam.Value as string;

                if (!string.IsNullOrEmpty(storedMdHash))
                {
                    // Compare the stored hash with the request hash
                    if (string.Equals(storedMdHash, requestMdHash, StringComparison.OrdinalIgnoreCase))
                    {
                        isValid = true;
                    }
                }
            }

            return isValid;
        }
    }
}
