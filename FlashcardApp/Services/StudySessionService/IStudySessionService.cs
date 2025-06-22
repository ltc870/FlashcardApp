using FlashcardApp.Models.Flashcard;

namespace FlashcardApp.Services.StudySessionService;

public interface IStudySessionService
{
    public void RunStudySession();
    public void GetAllStudySessions();
    public void GetStudySessionById(int studySessionId);
}