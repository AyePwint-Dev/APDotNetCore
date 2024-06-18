using APDotNetCoreConsoleApp.AdoDotNetExamples;
using APDotNetCoreConsoleApp.DapperExamples;
using APDotNetCoreConsoleApp.EFCoreExamples;
using APDotNetCoreConsoleApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

Console.WriteLine("Hello, World!");

//Day1

//npm
//pub.dev
//nuget
//SqlConnection

//SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
//stringBuilder.DataSource = ".";//server name
//stringBuilder.InitialCatalog = "DotNetTrainingBatch4"; //db name
//stringBuilder.UserID = "sa";
//stringBuilder.Password = "12345";
//SqlConnection connection = new SqlConnection(stringBuilder.ConnectionString);

//connection.Open();
//Console.WriteLine("Connection open");

//string query = "Select * from tbl_blog";
//SqlCommand cmd = new SqlCommand(query, connection);
//SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
//DataTable dt = new DataTable();
//sqlDataAdapter.Fill(dt);


//connection.Close();
//Console.WriteLine("Connection close");

////dataset => datatable [store one or more datatable]
////datatable => datarow
////datarow => datacolumn

//foreach (DataRow dr in dt.Rows)
//{
//    Console.WriteLine(dr["BlogId"]);
//    Console.WriteLine(dr["BlogTitle"]);
//    Console.WriteLine(dr["BlogAuthor"]);
//    Console.WriteLine(dr["BlogContent"]);
//    Console.WriteLine("-------------------------");
//}

//Day2 
//Ado.Net Read
//CRUD
////AdoDotNetExample adoDotNetExample = new AdoDotNetExample();

//adoDotNetExample.Create("title 1","Author 1","Content 1");
//adoDotNetExample.Update(6,"title update", "Author update", "Content update");
//adoDotNetExample.Delete(7);
////adoDotNetExample.Edit(9);
///

//DapperExample dapperExample = new DapperExample();
//dapperExample.Run();

//Day 4
//EFCoreExample eFCoreExample = new EFCoreExample();
//eFCoreExample.Run();
//Console.ReadKey();

var connectionString = ConnectionStrings.SqlConnectionStringBuilder.ConnectionString;
var sqlConnectionStringBuilder = new SqlConnectionStringBuilder(connectionString);


var serviceProvider = new ServiceCollection()
    .AddScoped<AdoDotNetExample>(n => new AdoDotNetExample(sqlConnectionStringBuilder))
    .AddScoped<DapperExample>(n => new DapperExample(sqlConnectionStringBuilder))
    .AddDbContext<AppDbContext>(opt =>
    {
        opt.UseSqlServer(connectionString);       
    })
    .AddScoped<EFCoreExample>()
    .BuildServiceProvider();

//AppDbContext db = serviceProvider.GetRequiredService<AppDbContext>();
var adoDotNnetExample = serviceProvider.GetRequiredService<AdoDotNetExample>();
adoDotNnetExample.Read();

var dapperExample = serviceProvider.GetRequiredService<DapperExample>();
dapperExample.Run();

Console.ReadLine();