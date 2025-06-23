using FlashcardApp.Models.Flashcard;
using FlashcardApp.Utils;
using Microsoft.Data.SqlClient;

namespace FlashcardApp.Repository.FlashcardRepository;

public class FlashcardRepository : IFlashcardRepository
{
    public List<Flashcard> ViewAllFlashcards()
    {
        List<Flashcard> flashcardData = new List<Flashcard>();

        using var connection = DbHelper.DbHelper.GetConnection();
        connection.Open();
        var cmd = new SqlCommand(@"SELECT f.FlashcardId, f.FrontText, f.BackText, f.StackId, s.Name AS StackName FROM Flashcards f JOIN Stacks s ON f.StackId = s.StackId", connection);
        try
        {
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    flashcardData.Add(
                        new Flashcard
                        {
                            FlashcardId = reader.GetInt32(0),
                            FrontText = reader.GetString(1),
                            BackText = reader.GetString(2),
                            StackId = reader.GetInt32(3),
                            StackName = reader.GetString(4)
                        }
                    );
                }
            }
            else
            {
                Console.WriteLine("No Flashcards found");
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"An error occurred while fetching flashcards: {ex.Message}");
        }
        finally
        {
            connection.Close();
        }

        return flashcardData;
    }

    public void CreateFlashcard(int stackId, string frontText, string backText)
    {
        Flashcard flashcard = new Flashcard
        {
            StackId = stackId,
            FrontText = frontText,
            BackText = backText
        };

        using var connection = DbHelper.DbHelper.GetConnection();
        connection.Open();
        var cmd = new SqlCommand(
            "INSERT INTO Flashcards (FrontText, BackText, StackId) VALUES (@FrontText, @BackText, @StackId)",
            connection);
        cmd.Parameters.AddWithValue("@FrontText", flashcard.FrontText);
        cmd.Parameters.AddWithValue("@BackText", flashcard.BackText);
        cmd.Parameters.AddWithValue("@StackId", flashcard.StackId);

        try
        {
            cmd.ExecuteNonQuery();
            Console.WriteLine("Flashcard created successfully!!");

        }
        catch (SqlException ex)
        {
            Console.WriteLine($"An error occurred while creating the flashcard: {ex.Message}");
            throw;
        }
        finally
        {
            connection.Close();
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }

    public void UpdateFlashcardById(int flashcardId, string? frontText, string? backText)
    {
        using var connection = DbHelper.DbHelper.GetConnection();
        connection.Open();

        var setClauses = new List<string>();
        if (frontText != null) { setClauses.Add("FrontText = @frontText"); }
        if (backText != null) { setClauses.Add("BackText = @backText"); }

        if (setClauses.Count == 0)
        {
            Console.WriteLine("No fields to update. Operation cancelled.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
            return;
        }

        var sql = $"UPDATE Flashcards SET {string.Join(", ", setClauses)} WHERE FlashcardId = @FlashcardId";
        var updateCmd = new SqlCommand(sql, connection);
        if (frontText != null) { updateCmd.Parameters.AddWithValue("@FrontText", frontText); }
        if (backText != null) { updateCmd.Parameters.AddWithValue("@BackText", backText); }
        
        updateCmd.Parameters.AddWithValue("@FlashcardId", flashcardId);
        
        try
        {
            int rowsAffected = updateCmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                Console.WriteLine("Flashcard updated successfully! ");
            }
            else
            {
                Console.WriteLine("No flashcard found with the specified ID.");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while updating the flashcard: {e.Message}");
            throw;
        }
        finally
        {
            connection.Close();
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }

    public void DeleteFlashcardById(int flashcardId)
    {
        using var connection = DbHelper.DbHelper.GetConnection();
        connection.Open();
        
        var cmd = new SqlCommand("DELETE FROM Flashcards WHERE FlashcardId = @FlashcardId", connection);
        cmd.Parameters.AddWithValue("@FlashcardId", flashcardId);

        try
        {
            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                Console.WriteLine("Flashcard deleted successfully!");
            }
            else
            {
                Console.WriteLine("No flashcard found with the specified ID.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while updating the flashcard: {ex.Message}");
            throw;
        }
        finally
        {
            connection.Close();
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }
}