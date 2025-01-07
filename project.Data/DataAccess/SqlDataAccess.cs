using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace project.Data.DataAccess
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }
    public async Task<IEnumerable<T>> GetData<T, P>(string spName,
        P parametres,string connectionId="conn")
        {
            using IDbConnection connection = new SqlConnection
                (_config.GetConnectionString(connectionId));
            return await connection.QueryAsync<T>(spName, parametres,
                commandType: CommandType.StoredProcedure);
        }
    public async Task SaveData<T>(string spName,
        T parametres, string connectionId = "conn")
        {
            using IDbConnection connection = new SqlConnection
                (_config.GetConnectionString(connectionId));
            await connection.ExecuteAsync(spName, parametres,
                commandType: CommandType.StoredProcedure);

        }

    }
}
