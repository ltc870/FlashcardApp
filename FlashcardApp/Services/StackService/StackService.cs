using FlashcardApp.Models.Stack;
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
        
        List<Stack> stackData = _stackRepository.ViewAllStacks();
        
        Console.WriteLine("<------------------------------------------>\n");
        foreach (Stack stack in stackData)
        {
            Console.WriteLine($"{stack.StackId}. {stack.Name}");
        }
        Console.WriteLine("\n<------------------------------------------>");
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }
    
    public void CreateStack()
    {
        Console.Clear();
        Console.WriteLine("Name the stack you would like to create, or enter 0 to cancel operation:");
        string? stackName = Console.ReadLine()?.Trim();
        
        if (stackName == "0")
        {
            Console.WriteLine("Canceling operation. Press any key to continue...");
            Console.ReadKey();
            return;
        }
        
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
        
        Console.WriteLine("Enter the ID of the stack you want to update or enter 0 to cancel operation:");

        string? newStackId;
        int newStackIdValue;
        var validation = new Validation.Validation(new StackRepository());
        

        do
        {
            newStackId = Console.ReadLine()?.Trim();

            bool stackIdExists = validation.DoesStackIdExist(newStackId);
            
            if (newStackId == "0")
            {
                Console.WriteLine("Canceling operation. Press any key to continue...");
                Console.ReadKey();
                return;
            }
            
            if (!stackIdExists)
            {
                Console.WriteLine("Stack ID does not exist. Canceling operation. Press any key to continue...");
                Console.ReadLine();
                return;
            }
            
            if (string.IsNullOrEmpty(newStackId) || !int.TryParse(newStackId, out newStackIdValue))
            {
                Console.WriteLine("Invalid input. Please enter a valid stack ID.");
            }
        } while (string.IsNullOrEmpty(newStackId) || !int.TryParse(newStackId, out newStackIdValue));
        
        Console.WriteLine("Enter the new name for the stack or enter 0 to cancel operation:");
        
        string? newStackName = Console.ReadLine()?.Trim();
        
        if (newStackName == "0")
        {
            Console.WriteLine("Canceling operation. Press any key to continue...");
            Console.ReadKey();
            return;
        }
        
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
        
        Console.WriteLine("Enter the ID of the stack you want to delete or enter 0 to cancel operation:");
        string? input;
        int itemId;
        var validation = new Validation.Validation(new StackRepository());
        
        do
        {
            input = Console.ReadLine()?.Trim();
            
            bool stackIdExists = validation.DoesStackIdExist(input);
            
            if (input == "0")
            {
                Console.WriteLine("Canceling operation. Press any key to continue...");
                Console.ReadKey();
                return;
            }
            
            if (!stackIdExists)
            {
                Console.WriteLine("Stack ID does not exist. Canceling operation. Press any key to continue...");
                Console.ReadLine();
                return;
            }
            
            
            if (string.IsNullOrEmpty(input) || !int.TryParse(input, out itemId) || itemId <= 0)
            {
                Console.WriteLine("Invalid ID. Please enter a valid positive integer ID:");
            }
        } while (string.IsNullOrEmpty(input) || !int.TryParse(input, out itemId) || itemId <= 0);
        
        _stackRepository.DeleteStackById(itemId);
    }
}