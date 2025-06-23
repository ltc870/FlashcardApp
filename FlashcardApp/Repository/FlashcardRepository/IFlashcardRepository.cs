using FlashcardApp.Models.Flashcard;

namespace FlashcardApp.Repository.FlashcardRepository;

public interface IFlashcardRepository
{
    public List<Flashcard> ViewAllFlashcards();
    public void CreateFlashcard(int stackId, string frontText, string backText);
    public void UpdateFlashcardById(int flashcardId, string? frontText, string? backText);
    public void DeleteFlashcardById(int flashcardId);
}