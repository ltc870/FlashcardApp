using FlashcardApp.Models.Flashcard;
using FlashcardApp.Models.StudySession;
using Microsoft.Data.SqlClient;

namespace FlashcardApp.Repository.StudySessionRepository;

public class StudySessionRepository : IStudySessionRepository
{
    public void SaveStudySession(StudySession studySession)
    {
        throw new NotImplementedException();
    }

    public List<Flashcard> GetFlashcardsForStudySession(int stackId)
    {
        List<Flashcard> flashcardData = new List<Flashcard>();

        using var connection = DbHelper.DbHelper.GetConnection();
        connection.Open();
        var cmd = new SqlCommand(
            @"SELECT f.FlashcardId, f.FrontText, f.BackText, s.Name AS StackName 
                     FROM Flashcards f 
                     JOIN Stacks s ON f.StackId = s.StackId
                     WHERE f.StackId = @StackId", connection);
        cmd.Parameters.AddWithValue("@StackId", stackId);
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
                            StackName = reader.GetString(3)
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

    public void GetAllStudySessions()
    {
        throw new NotImplementedException();
    }
}