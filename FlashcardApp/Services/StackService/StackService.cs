using FlashcardApp.Repository.StackRepository;

namespace FlashcardApp.Services.StackService;

public class StackService : IStackService
{
    private readonly IStackRepository _stackRepository;
    
    public StackService(IStackRepository stackRepository)
    {
        _stackRepository = stackRepository;
    }

    public void ViewAllStacks()
    {
        Console.Clear();
        Console.WriteLine("Fetching all stacks...");
        _stackRepository.ViewAllStacks();
    }
    
    public void CreateStack()
    {
        Console.Clear();
        Console.WriteLine("What is the name of the stack you would like to create?");
        string? stackName = Console.ReadLine()?.Trim();
        
        if (string.IsNullOrEmpty(stackName))
        {
            Console.WriteLine("Stack name cannot be empty. Please try again.");
            stackName = Console.ReadLine()?.Trim();
        }
        _stackRepository.CreateStack(stackName);
    }

    public void UpdateStackById()
    {
        Console.Clear();
        Console.WriteLine("Updating a stack...");
        ViewAllStacks();
        
        Console.WriteLine("Enter the ID of the stack you want to update:");

        string? newStackId;
        int newStackIdValue;

        do
        {
            newStackId = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(newStackId) || !int.TryParse(newStackId, out newStackIdValue))
            {
                Console.WriteLine("Invalid input. Please enter a valid stack ID.");
            }
        } while (string.IsNullOrEmpty(newStackId) || !int.TryParse(newStackId, out newStackIdValue));
        
        Console.WriteLine("Enter the new name for the stack:");
        
        string? newStackName = Console.ReadLine()?.Trim();
        
        if (string.IsNullOrEmpty(newStackName))
        {
            Console.WriteLine("Stack name cannot be empty. Please try again.");
            newStackName = Console.ReadLine()?.Trim();
        }
        
        if (newStackName != null) _stackRepository.UpdateStackById(newStackIdValue, newStackName);
    }

    public void DeleteStackById()
    {
        Console.Clear();
        Console.WriteLine("Delete a stack...");
        ViewAllStacks();
        
        Console.WriteLine("Enter the ID of the stack you want to delete:");
        string? input;
        int itemId;
        
        do
        {
            input = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(input) || !int.TryParse(input, out itemId) || itemId <= 0)
            {
                Console.WriteLine("Invalid ID. Please enter a valid positive integer ID:");
            }
        } while (string.IsNullOrEmpty(input) || !int.TryParse(input, out itemId) || itemId <= 0);
        
        _stackRepository.DeleteStackById(itemId);
    }
}