using Microsoft.Extensions.Options;
using MobileLegendsAPI.Models;
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace MobileLegendsAPI.Db
{
    public class DbManager
    {
        public static QueryFactory Connect()
        {
            IConfiguration configuration = new ConfigurationBuilder()
              .SetBasePath(System.AppContext.BaseDirectory)
              .AddJsonFile("appsettings.json")
              .Build();

            var dbSettings = new DbSettings();

            configuration.GetSection("Connection").Bind(dbSettings);

            string host = dbSettings.Host;
            string data = dbSettings.Database;
            string name = dbSettings.Username;
            string password = dbSettings.Password;


            string connectionString = $"Data Source={host};Initial Catalog={data};User Id={name};Password={password};";

            var connection = new SqlConnection(connectionString);

            var compiler = new SqlServerCompiler();

            var db = new QueryFactory(connection, compiler);

            return db;
            
        }
    }
}
