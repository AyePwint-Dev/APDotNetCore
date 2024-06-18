using System.Data.SqlClient;

namespace DotNetTrainingBatch4.WinFormsApp;

internal static class ConnectionStrings
{
    public static SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
    {
        DataSource = ".",
        InitialCatalog = "DotNetTrainingBatch4",        
        UserID = "sa",
        Password = "12345",
        TrustServerCertificate = true
    };
}