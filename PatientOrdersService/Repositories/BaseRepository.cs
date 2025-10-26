using System.Data;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dapper;
using PatientOrdersService.Data;

namespace PatientOrdersService.Repositories
{
    public class BaseRepository<T> where T : class
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly string _tableName;
        private readonly string _whereClauseWithKey;
        private readonly List<PropertyInfo> _keyProperties;
        private readonly List<PropertyInfo> _insertKeyProperties;
        private readonly List<PropertyInfo> _nonKeyProperties;

        public BaseRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;

            // [Table] 資料表名稱
            var tableAttr = typeof(T).GetCustomAttribute<TableAttribute>();
            _tableName = tableAttr?.Name ?? typeof(T).Name;

            var _properties = typeof(T).GetProperties();

            // 主鍵屬性
            _keyProperties = _properties
                .Where(p => p.GetCustomAttribute<KeyAttribute>() != null)
                .ToList();

            if (_keyProperties.Count == 0)
                throw new InvalidOperationException($"No [Key] property found in {typeof(T).Name}");

            // 主鍵 where 條件
            _whereClauseWithKey = string.Join(" AND ", _keyProperties.Select(p => $"{p.Name} = @{p.Name}"));

            // 非主鍵屬性（新增和更新的欄位）
            _nonKeyProperties = _properties
                .Where(p => p.GetCustomAttribute<KeyAttribute>() == null)
                .ToList();

            // 可新增的屬性（非自動產生主鍵 + 所有非主鍵欄位）
            _insertKeyProperties = _nonKeyProperties
                .Concat(
                    _keyProperties.Where(p =>
                    {
                        var dbGenAttr = p.GetCustomAttribute<DatabaseGeneratedAttribute>();
                        return dbGenAttr?.DatabaseGeneratedOption != DatabaseGeneratedOption.Identity;
                    })
                )
                .ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using var conn = await _connectionFactory.CreateConnectionAsync();
        
            var sql = $"SELECT * FROM {_tableName}";
            return await conn.QueryAsync<T>(sql);
        }

        public async Task<IEnumerable<T>> GetByKeyAsync(object keyObject)
        {
            using var conn = await _connectionFactory.CreateConnectionAsync();
        
            var props = keyObject.GetType().GetProperties();
            var whereClause = string.Join(" AND ", props.Select(p => $"{p.Name} = @{p.Name}"));
            var parameters = new DynamicParameters(keyObject);
            var sql = $"SELECT * FROM {_tableName} WHERE {whereClause};";

            return await conn.QueryAsync<T>(sql, parameters);
        }

        public async Task<int> InsertAsync(T entity)
        {
            using var conn = await _connectionFactory.CreateConnectionAsync();
        
            var cols = string.Join(", ", _insertKeyProperties.Select(p => $"{p.Name}"));
            var vals = string.Join(", ", _insertKeyProperties.Select(p => "@" + p.Name));
            var sql = $"INSERT INTO {_tableName} ({cols}) VALUES ({vals});";

            return await conn.ExecuteAsync(sql, entity);
        }

        public async Task<int> UpdateAsync(T entity)
        {
            using var conn = await _connectionFactory.CreateConnectionAsync();
        
            var setClause = string.Join(", ", _nonKeyProperties.Select(p => $"{p.Name} = @{p.Name}"));
            var sql = $"UPDATE {_tableName} SET {setClause} WHERE {_whereClauseWithKey}";
            return await conn.ExecuteAsync(sql, entity);
        }

        public async Task<int> DeleteAsync(T entity)
        {
            using var conn = await _connectionFactory.CreateConnectionAsync();

            var sql = $"DELETE FROM {_tableName} WHERE {_whereClauseWithKey}";
            return await conn.ExecuteAsync(sql, entity);
        }
    }
}
