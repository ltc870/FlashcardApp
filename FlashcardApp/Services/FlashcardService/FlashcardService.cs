using FlashcardApp.Models.Flashcard;
using FlashcardApp.Repository.FlashcardRepository;
using FlashcardApp.Repository.StackRepository;
using FlashcardApp.Utils;
using static FlashcardApp.Validation.Validation;

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
        List<Flashcard> flashcardData = _flashcardRepository.ViewAllFlashcards();
        
        Console.WriteLine("<------------------------------------------>\n");

        
        foreach (var flashcard in flashcardData)
        {
            var flashcardDto = ModelToDtoMapperUtil.MapFlashCardToDto(flashcard);
            
            Console.WriteLine($"Id: {flashcardDto.FlashcardId} Stack: {flashcardDto.StackName} - Front: {flashcardDto.FrontText} | Back: {flashcardDto.BackText}");
            
        }
        
        Console.WriteLine("\n<------------------------------------------>\n");
        
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }

    public void CreateFlashcard()
    {
        string? stackIdInput;
        string? frontText;
        string? backText;
        
        Console.Clear();
        Console.WriteLine("Creating a new flashcard...");
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
        StackService.StackService stackService = new StackService.StackService(new StackRepository());
        stackService.ViewAllStacks();

        int stackId;
        
        Console.WriteLine("Enter the Stack ID of the stack you want to add the flashcard to or enter 0 to cancel operation:");
        do
        {
            stackIdInput = Console.ReadLine()?.Trim();
            
            if (stackIdInput == "0")
            {
                Console.WriteLine("Canceling operation. Press any key to continue...");
                Console.ReadLine();
                return;
            }
            
            if (string.IsNullOrEmpty(stackIdInput) || !int.TryParse(stackIdInput, out stackId))
            {
                Console.WriteLine("Invalid input. Please enter a valid Stack ID.");
            }
        } while (string.IsNullOrEmpty(stackIdInput) || !int.TryParse(stackIdInput, out stackId));


        Console.WriteLine("Enter the front text of the flashcard or enter 0 to cancel operation:");
        do
        {
            frontText = Console.ReadLine()?.Trim();
            
            if (frontText == "0")
            {
                Console.WriteLine("Canceling operation. Press any key to continue...");
                Console.ReadLine();
                return;
            }
            
            if (string.IsNullOrEmpty(frontText))
            {
                Console.WriteLine("Front text cannot be empty. Please try again.");
            }
        } while (string.IsNullOrEmpty(frontText));

        
        Console.WriteLine("Enter the back text of the flashcard or enter 0 to cancel operation:");
        do
        {
            backText = Console.ReadLine()?.Trim();
            
            if (backText == "0")
            {
                Console.WriteLine("Canceling operation. Press any key to continue...");
                Console.ReadLine();
                return;
            }
            
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
        var validation = new Validation.Validation(_flashcardRepository);
        
        Console.Clear();
        Console.WriteLine("Updating flashcard...");
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
        ViewAllFlashcards();
        
        Console.WriteLine("Enter the ID of the flashcard you want to update or enter 0 to cancel the operation:");
        string? flashcardIdInput;
        do
        {
            flashcardIdInput = Console.ReadLine()?.Trim();
            
            bool flashcardIdExists = validation.DoesFlashcardIdExist(flashcardIdInput);
            
            if (flashcardIdInput == "0")
            {
                Console.WriteLine("Canceling operation. Press any key to continue...");
                Console.ReadLine();
                return;
            }
            
            if (!flashcardIdExists)
            {
                Console.WriteLine("Flashcard ID does not exist. Canceling operation. Press any key to continue...");
                Console.ReadLine();
                return;
            }
            
            if (string.IsNullOrEmpty(flashcardIdInput) || !int.TryParse(flashcardIdInput, out flashcardId))
            {
                Console.WriteLine("Invalid input. Please enter a valid Flashcard ID.");
            }
        } while (string.IsNullOrEmpty(flashcardIdInput) || !int.TryParse(flashcardIdInput, out flashcardId));
        
        Console.WriteLine("Enter the new front text of the flashcard, press Enter to keep it unchanged, or enter 0 to cancel operation:");
        var frontText = Console.ReadLine()?.Trim();
        
        if (frontText == "0")
        {
            Console.WriteLine("Canceling operation. Press any key to continue...");
            Console.ReadLine();
            return;
        }
        
        if (string.IsNullOrEmpty(frontText))
        {
            Console.WriteLine("No new changes.");
            frontText = null; // Keep unchanged
        }
        
        Console.WriteLine("Enter the new back text of the flashcard, press Enter to keep it unchanged, or enter 0 to cancel operation:");
        var backText = Console.ReadLine()?.Trim();
        
        if (backText == "0")
        {
            Console.WriteLine("Canceling operation. Press any key to continue...");
            Console.ReadLine();
            return;
        }
        
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
        
        Console.WriteLine("Enter the ID of the flashcard you want to delete or enter 0 to exit:");
        string? flashcardIdInput;
        int flashcardId;
        var validation = new Validation.Validation(_flashcardRepository);
        
        do
        {
            flashcardIdInput = Console.ReadLine()?.Trim();
            
            if (flashcardIdInput == "0")
            {
                Console.WriteLine("Exiting delete operation. Press any key to continue...");
                Console.ReadLine();
                return;
            }
            
            bool flashcardIdExists = validation.DoesFlashcardIdExist(flashcardIdInput);
            
            if (!flashcardIdExists)
            {
                Console.WriteLine("Flashcard ID does not exist. Canceling operation. Press any key to continue...");
                Console.ReadLine();
                return;
            }
            
            
            if (string.IsNullOrEmpty(flashcardIdInput) || !int.TryParse(flashcardIdInput, out flashcardId))
            {
                Console.WriteLine("Invalid input. Please enter a valid Flashcard ID.");
            }
        } while (string.IsNullOrEmpty(flashcardIdInput) || !int.TryParse(flashcardIdInput, out flashcardId));
        
        
        
        _flashcardRepository.DeleteFlashcardById(flashcardId);
    }
}