using System.Data;
using Npgsql;

namespace PatientOrdersService.Data
{
    /// <summary>
    /// PostgreSQL 連線工廠實作
    /// </summary>
    public class NpgsqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public NpgsqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        /// <summary>
        /// 建立新的 PostgreSQL 連線
        /// </summary>
        public IDbConnection CreateConnection()
        {
            var conn = new NpgsqlConnection(_connectionString);
            conn.Open();  
            return conn;
        }

        public async Task<IDbConnection> CreateConnectionAsync()
        {
            var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            return conn;
        }
    }
}
