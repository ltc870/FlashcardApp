using FlashcardApp.Models.Flashcard;
using FlashcardApp.Models.StudySession;

namespace FlashcardApp.Repository.StudySessionRepository;

public interface IStudySessionRepository
{
    public void SaveStudySession(StudySession studySession);
    public List<Flashcard> GetFlashcardsForStudySession(int stackId);
    public void GetAllStudySessions();
}