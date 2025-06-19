namespace FlashcardApp.Repository.FlashcardRepository;

public interface IFlashcardRepository
{
    public void ViewAllFlashcards();
    public void CreateFlashcard(int stackId, string frontText, string backText);
    public void UpdateFlashcardById(int flashcardId, string? frontText, string? backText);
    public void DeleteFlashcardById(int flashcardId);
}