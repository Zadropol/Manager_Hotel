using Hotel_Manager.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Manager.Infrastructure.Data
{
    public class DbConnectionFactory :IDbConnectionFactory
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public DbConnectionFactory(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("MySqlConnection")!;
        }

        public IDbConnection CreateConnection() =>
            new MySqlConnection(_connectionString);

    }
}
