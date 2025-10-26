using System.Data;

namespace PatientOrdersService.Data
{
    /// <summary>
    /// 資料庫連線工廠介面
    /// </summary>
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
        Task<IDbConnection> CreateConnectionAsync();
    }

}
