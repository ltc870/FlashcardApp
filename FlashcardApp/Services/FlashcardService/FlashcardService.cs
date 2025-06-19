using FlashcardApp.Repository.DbHelper;
using FlashcardApp.Repository.FlashcardRepository;
using FlashcardApp.Repository.StackRepository;

namespace FlashcardApp.Services.FlashcardService;

public class FlashcardService : IFlashcardService
{
    private readonly IFlashcardRepository _flashcardRepository;
    
    public FlashcardService(IFlashcardRepository flashcardRepository)
    {
        _flashcardRepository = flashcardRepository;
    }
    
    public void ViewAllFlashcards()
    {
        Console.Clear();
        Console.WriteLine("Viewing all flashcards...");
        _flashcardRepository.ViewAllFlashcards();
    }

    public void CreateFlashcard()
    {
        string? frontText;
        string? backText;
        
        Console.Clear();
        Console.WriteLine("Creating a new flashcard...");
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
        StackService.StackService stackService = new StackService.StackService(new StackRepository());
        stackService.ViewAllStacks();
       
        
        Console.WriteLine("Enter the Stack ID of the stack you want to add the flashcard to:");
        string? stackIdInput;
        int stackId;
        
        do
        {
            stackIdInput = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(stackIdInput) || !int.TryParse(stackIdInput, out stackId))
            {
                Console.WriteLine("Invalid input. Please enter a valid Stack ID.");
            }
        } while (string.IsNullOrEmpty(stackIdInput) || !int.TryParse(stackIdInput, out stackId));
        
        do
        {
            Console.WriteLine("Enter the front text of the flashcard:");
            frontText = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(frontText))
            {
                Console.WriteLine("Front text cannot be empty. Please try again.");
            }
        } while (string.IsNullOrEmpty(frontText));

        do
        {
            Console.WriteLine("Enter the back text of the flashcard:");
            backText = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(backText))
            {
                Console.WriteLine("Back text cannot be empty. Please try again.");
            }
        } while (string.IsNullOrEmpty(backText));
        
        _flashcardRepository.CreateFlashcard(stackId, frontText, backText);
    }

    public void UpdateFlashcardById()
    {
        int flashcardId;
        
        Console.Clear();
        Console.WriteLine("Updating flashcard...");
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
        ViewAllFlashcards();
        
        Console.WriteLine("Enter the ID of the flashcard you want to update:");
        string? flashcardIdInput;
        do
        {
            flashcardIdInput = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(flashcardIdInput) || !int.TryParse(flashcardIdInput, out flashcardId))
            {
                Console.WriteLine("Invalid input. Please enter a valid Flashcard ID.");
            }
        } while (string.IsNullOrEmpty(flashcardIdInput) || !int.TryParse(flashcardIdInput, out flashcardId));
        
        Console.WriteLine("Enter the new front text of the flashcard (or press Enter to keep it unchanged):");
        var frontText = Console.ReadLine()?.Trim();
        if (string.IsNullOrEmpty(frontText))
        {
            Console.WriteLine("No new changes.");
            frontText = null; // Keep unchanged
        }
        
        Console.WriteLine("Enter the new back text of the flashcard (or press Enter to keep it unchanged):");
        var backText = Console.ReadLine()?.Trim();
        if (string.IsNullOrEmpty(backText))
        {
            Console.WriteLine("No new changes.");
            backText = null; // Keep unchanged
        }
        
        _flashcardRepository.UpdateFlashcardById(flashcardId, frontText, backText);
    }

    public void DeleteFlashcardById()
    {
        Console.WriteLine("Deleting flashcard...");
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
        ViewAllFlashcards();
        
        Console.WriteLine("Enter the ID of the flashcard you want to delete:");
        string? flashcardIdInput;
        int flashcardId;
        
        do
        {
            flashcardIdInput = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(flashcardIdInput) || !int.TryParse(flashcardIdInput, out flashcardId))
            {
                Console.WriteLine("Invalid input. Please enter a valid Flashcard ID.");
            }
        } while (string.IsNullOrEmpty(flashcardIdInput) || !int.TryParse(flashcardIdInput, out flashcardId));
        
        _flashcardRepository.DeleteFlashcardById(flashcardId);
    }
}