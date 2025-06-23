using FlashcardApp.Models.Flashcard;
using FlashcardApp.Repository.FlashcardRepository;

namespace FlashcardApp.Validation;

public class Validation
{
    
    private readonly IFlashcardRepository _flashcardRepository;
    
    public Validation(IFlashcardRepository flashcardRepository)
    {
        _flashcardRepository = flashcardRepository;
    }
    
    public bool DoesFlashcardIdExist(string flashcardIdInput)
    {
        List<Flashcard> flashcards = _flashcardRepository.ViewAllFlashcards();

        foreach (Flashcard flashcard in flashcards)
        {
            if (flashcard.FlashcardId.ToString() == flashcardIdInput)
            {
                return true; // Flashcard ID exists
            }
            return false;
        }
        return true;
    }
}