using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using System.Reflection.Metadata;

namespace APDotNetTrainingBatch4.Shared
{
    public class AdoDotNetService
    {
        private readonly string _connectionString;
        public AdoDotNetService(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<T> Query<T>(string query, params AdoDotNetRequestParameter[]? parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            //# Type I
            //if(parameters.Length > 0)
            //{
            //    foreach(var item in parameters)
            //    {
            //        cmd.Parameters.AddWithValue(item.Name, item.Value);
            //    }

            //}

            //# Type II
            if (parameters is not null && parameters.Length > 0)
            {
                // # Two ways to write add paramters
                //cmd.Parameters.AddRange(parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray());
                // Or
                var parametersArray = parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray();
                cmd.Parameters.AddRange(parametersArray);
            }
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connection.Close();
            string json = JsonConvert.SerializeObject(dt); //C# to Json
            List<T> lst = JsonConvert.DeserializeObject<List<T>>(json)!; //Json to C#
            return lst;
        }
        public T QueryFirstOrDefault<T>(string query, params AdoDotNetRequestParameter[]? parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            //# Type I
            //if(parameters.Length > 0)
            //{
            //    foreach(var item in parameters)
            //    {
            //        cmd.Parameters.AddWithValue(item.Name, item.Value);
            //    }

            //}

            //# Type II
            if (parameters is not null && parameters.Length > 0)
            {
                // # Two ways to write add paramters
                //cmd.Parameters.AddRange(parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray());
                // Or
                var parametersArray = parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray();
                cmd.Parameters.AddRange(parametersArray);
            }
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connection.Close();
            string json = JsonConvert.SerializeObject(dt); //C# to Json
            List<T> lst = JsonConvert.DeserializeObject<List<T>>(json)!; //Json to C#
            return lst[0];
        }
        public int Execute(string query, params AdoDotNetRequestParameter[]? parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            //# Type I
            if (parameters is not null && parameters.Length > 0)
            {
                foreach (var item in parameters)
                {
                    cmd.Parameters.AddWithValue(item.Name, item.Value);
                }

            }
            int result = cmd.ExecuteNonQuery();
            connection.Close();

            return result;
        }

        //private BlogModel? FindById(int id)
        //{
        //    string query = "Select * from tbl_blog where blogid = @BlogId";
        //    SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);

        //}

        public class AdoDotNetRequestParameter
        {
            public AdoDotNetRequestParameter() { }
            public AdoDotNetRequestParameter(string name, object value)
            {
                Name = name;
                Value = value;
            }
            public string Name { get; set; }
            public object Value { get; set; }
        }
    }
}
