using Microsoft.Data.SqlClient;
using System.Configuration;

namespace FlashcardApp.Repository.DbHelper;

public class DbHelper
{
    public static SqlConnection GetConnection()
    {
        var connectionString = ConfigurationManager.ConnectionStrings["FlashcardAppDb"].ConnectionString;
        return new SqlConnection(connectionString);
    }
}