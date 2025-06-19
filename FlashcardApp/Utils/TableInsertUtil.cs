using FlashcardApp.Repository.DbHelper;
using Microsoft.Data.SqlClient;

namespace FlashcardApp.Utils;

public class TableInsertUtil
{
    public static void CreateTables()
    {
        string sql = @"
                IF OBJECT_ID('Stacks', 'U') IS NULL
                CREATE TABLE Stacks (
                    StackId INT PRIMARY KEY IDENTITY(1,1),
                    Name NVARCHAR(100) UNIQUE NOT NULL
                );

                IF OBJECT_ID('Flashcards', 'U') IS NULL
                CREATE TABLE Flashcards (
                    FlashcardId INT PRIMARY KEY IDENTITY(1,1),
                    FrontText NVARCHAR(MAX) NOT NULL,
                    BackText NVARCHAR(MAX) NOT NULL,
                    StackId INT NOT NULL,
                    FOREIGN KEY (StackId) REFERENCES Stacks(StackId) ON DELETE CASCADE
                );

                IF OBJECT_ID('StudySessions', 'U') IS NULL
                CREATE TABLE StudySessions (
                    StudySessionId INT PRIMARY KEY IDENTITY(1,1),
                    Date DATETIME NOT NULL,
                    Score INT NOT NULL,
                    StackId INT NOT NULL,
                    FOREIGN KEY (StackId) REFERENCES Stacks(StackId) ON DELETE CASCADE
                );";

        var connection = DbHelper.GetConnection();
        connection.Open();
        
        using var command = new SqlCommand(sql, connection);
        try
        {
            command.ExecuteNonQuery();
            Console.WriteLine("Database and tables created successfully.");
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"An error occurred while creating the database: {ex.Message}");
        }
        finally
        {
            connection.Close();
        }
    }
}