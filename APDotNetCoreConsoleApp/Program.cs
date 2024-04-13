using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

Console.WriteLine("Hello, World!");


//npm
//pub.dev
//nuget
//SqlConnection

SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
stringBuilder.DataSource = ".";//server name
stringBuilder.InitialCatalog = "DotNetTrainingBatch4"; //db name
stringBuilder.UserID = "sa";
stringBuilder.Password = "12345";
SqlConnection connection = new SqlConnection(stringBuilder.ConnectionString);

connection.Open();
Console.WriteLine("Connection open");

string query = "Select * from tbl_blog";
SqlCommand cmd = new SqlCommand(query, connection);
SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
DataTable dt = new DataTable();
sqlDataAdapter.Fill(dt);


connection.Close();
Console.WriteLine("Connection close");

//dataset => datatable [store one or more datatable]
//datatable => datarow
//datarow => datacolumn

foreach (DataRow dr in dt.Rows)
{
    Console.WriteLine(dr["BlogId"]);
    Console.WriteLine(dr["BlogTitle"]);
    Console.WriteLine(dr["BlogAuthor"]);
    Console.WriteLine(dr["BlogContent"]);
    Console.WriteLine("-------------------------");
}

Console.ReadKey();

