using FlashcardApp.Models.Flashcard;
using FlashcardApp.Models.StudySession;
using Microsoft.Data.SqlClient;

namespace FlashcardApp.Repository.StudySessionRepository;

public class StudySessionRepository : IStudySessionRepository
{
    public void SaveStudySession(StudySession studySession)
    {
        using var connection = DbHelper.DbHelper.GetConnection();
        connection.Open();
        var cmd = new SqlCommand(
            @"INSERT INTO StudySessions (Date, Score, StackId) 
              VALUES (@Date, @Score, @StackId)", connection);
        cmd.Parameters.AddWithValue("@Date", studySession.Date);
        cmd.Parameters.AddWithValue("@Score", studySession.Score);
        cmd.Parameters.AddWithValue("@StackId", studySession.StackId);

        try
        {
            cmd.ExecuteNonQuery();
            Console.WriteLine("Study session saved successfully.");
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"An error occurred while saving the study session: {ex.Message}");
        }
        finally
        {
            connection.Close();
        }
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
        List<StudySession> studySessionData = new  List<StudySession>();
        using var connection = DbHelper.DbHelper.GetConnection();
        connection.Open();
        var cmd = new SqlCommand(@"SELECT ss.StudySessionId, ss.Date, ss.Score, s.Name As StackName 
                                          FROM StudySessions ss 
                                          JOIN Stacks s ON ss.StackId = s.StackId", connection);
        try
        {
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                Console.WriteLine("Study Sessions:");
                while (reader.Read())
                {
                    studySessionData.Add(
                        new StudySession
                        {
                            StudySessionId = reader.GetInt32(0),
                            Date = reader.GetDateTime(1),
                            Score = reader.GetInt32(2),
                            StackName = reader.GetString(3)
                        }
                    );
                }
            }
            else
            {
                Console.WriteLine("No study sessions found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while fetching study sessions: {ex.Message}");
            throw;
        }
        finally
        {
            connection.Close();
        }
        
        Console.WriteLine("<------------------------------------------>\n");
        
        foreach (var session in studySessionData)
        {
            Console.WriteLine($"Id: {session.StudySessionId} Date: {session.Date.ToShortDateString()} Score: {session.Score} StackId: {session.StackName}");
        }
        
        Console.WriteLine("\n<------------------------------------------>\n");
    }
}