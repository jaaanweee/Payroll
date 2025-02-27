using System.Collections.Generic;
using System.Threading.Tasks;

namespace project.Data.DataAccess
{
    public interface ISqlDataAccess
    {
        Task<int> ExecuteAsync(string v, object parameters, string connectionId = "conn");
        Task<IEnumerable<T>> GetData<T, P>(string spName,
        P parametres, string connectionId = "conn");

        Task SaveData<T>(string spName,
        T parametres, string connectionId = "conn");

      
    }
}