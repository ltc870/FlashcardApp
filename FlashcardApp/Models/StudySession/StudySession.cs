namespace FlashcardApp.Models.StudySession;

public class StudySession
{
    public int StudySessionId { get; set; }
    public DateTime Date { get; set; }
    public int Score { get; set; }
    public int StackId { get; set; }
}