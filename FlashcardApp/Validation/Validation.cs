using FlashcardApp.Models.Flashcard;
using FlashcardApp.Models.Stack;
using FlashcardApp.Repository.FlashcardRepository;
using FlashcardApp.Repository.StackRepository;

namespace FlashcardApp.Validation;

public class Validation
{
    
    private readonly IFlashcardRepository _flashcardRepository;
    private readonly IStackRepository _stackRepository;
    
    public Validation(IFlashcardRepository flashcardRepository)
    {
        _flashcardRepository = flashcardRepository;
    }

    public Validation(IStackRepository stackRepository)
    {
        _stackRepository = stackRepository;
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
        }
        return false; // Flashcard ID does not exist
    }
    
    public bool DoesStackIdExist(string stackIdInput)
    {
        List<Stack> stacks = _stackRepository.ViewAllStacks();

        foreach (Stack stack in stacks)
        {
            if (stack.StackId.ToString() == stackIdInput)
            {
                return true; // Stack ID exists
            }
        }
        return false; // Stack ID does not exist
    }
}