using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.SqlServer.Dac;

namespace MusicCatalog.IntegrationTests
{
    /// <summary>
    /// Setting configuration for a test database
    /// </summary>
    public class DatabaseConfiguration
    {
        /// <summary>
        /// Connection string
        /// </summary>
        private string _connectionString;

        /// <summary>
        /// Configuration properties
        /// </summary>
        private readonly IConfiguration _configuration;

        public DatabaseConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        /// <summary>
        /// Deploys the test database from file
        /// </summary>
        public void DeployTestDatabase()
        {
            var dacOptions = new DacDeployOptions
            {
                CreateNewDatabase = true,
                IgnoreAuthorizer = true,
                IgnoreUserSettingsObjects = true
            };

            var dacService = new DacServices(_connectionString);
            var dacPacPath = _configuration.GetSection("appSettings")["dacpacFilePath"];
            dacPacPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + dacPacPath;

            if (File.Exists(dacPacPath))
            {
                using (var dacPackage = DacPackage.Load(dacPacPath))
                {
                    dacService.Deploy(dacPackage, "TestMusicCatalog", true, dacOptions);
                }
            }
            else
            {
                throw new ConfigurationErrorsException($"Error load database from dacpac file.({AppDomain.CurrentDomain.SetupInformation.ApplicationBase})");
            }
        }

        /// <summary>
        /// Drops the test database
        /// </summary>
        public void DropTestDatabase()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = @"
                        USE master;
                        drop database[TestMusicCatalog]";
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
