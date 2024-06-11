using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace DotNetTrainingBatch4.Shared
{
    public class DapperService
    {
        private readonly string _connectionString;
        public DapperService(string connectionString) { 
            _connectionString = connectionString;
        }
        public List<T> Query<T>(string query, object? param  = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            //passing param case: if(param !=null) => include param , 
            //query line can do both for that condition
            var lst = db.Query<T>(query, param).ToList();
            return lst;
        }
        public T QueryFirstOrDefault<T>(string query, object? param = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            //passing param case: if(param !=null) => include param , 
            //query line can do both for that condition
            var item = db.Query<T>(query, param).FirstOrDefault();
            return item;
        }
        public int Execute(string query, object? param = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            //Can't check the value after execute
            //return db.Execute(query, param);
            var result = db.Execute(query, param);
            return result;
        }
    }
}