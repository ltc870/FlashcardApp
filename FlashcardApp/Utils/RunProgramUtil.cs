using FlashcardApp.Repository.FlashcardRepository;
using FlashcardApp.Services.FlashcardService;
using FlashcardApp.Services.StackService;

namespace FlashcardApp.Utils;

public class RunProgramUtil
{
    public static void StartProgram()
    {
        Console.WriteLine("\nWelcome to the Flashcard App!\n");
        Console.WriteLine("This app allows you to create and manage flashcards for studying.");
        Console.WriteLine("You can create stacks of flashcards, add flashcards to those stacks, and review them.");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
        Console.WriteLine("What would you like to do?");
        UserOptions();
    }

    public static void UserOptions()
    {
        bool closeProgram = false;

        while (closeProgram == false)
        {
            Console.WriteLine("Main Menu:");
            Console.WriteLine("<---------------------------------------------------------->\n");
            Console.WriteLine("Type 0 to Close Program.");
            Console.WriteLine("Type 1 Manage Stacks");
            Console.WriteLine("Type 2 Manage Flashcards");
            Console.WriteLine("Type 3 Study");
            Console.WriteLine("Type 4 View Study Session History");
            Console.WriteLine("\n<---------------------------------------------------------->");

            string userInput = Console.ReadLine();
            
            switch (userInput)
            {
                case "0":
                    Console.Clear();
                    Console.WriteLine("\nGoodbye!\n");
                    closeProgram = true;
                    Environment.Exit(0);
                    break;
                case "1":
                    ManageStacks();
                    break;
                 case "2":
                     ManageFlashcards();
                     break;
                // case "3":
                //     Study();
                //     break;
                // case "4":
                //     ViewStudySessionHistory();
                //     break;
                default:
                    Console.WriteLine("\nYou entered an invalid option, please try again.");
                    break;
            }
        }
    }

    private static void ManageStacks()
    {
        StackService stackService = new StackService(new Repository.StackRepository.StackRepository());
        bool closeManageStacks = false;

        while (closeManageStacks == false)
        {
            Console.Clear();
            Console.WriteLine("Manage Stacks Menu:");
            Console.WriteLine("<---------------------------------------------------------->\n");
            Console.WriteLine("Type 0 to Return to Main Menu.");
            Console.WriteLine("Type 1 to View All Stacks");
            Console.WriteLine("Type 2 to Create a New Stack");
            Console.WriteLine("Type 3 to Edit a Stack");
            Console.WriteLine("Type 4 to Delete a Stack");
            Console.WriteLine("\n<---------------------------------------------------------->");
            
            string userInput = Console.ReadLine();
            
            switch (userInput)
            {
                case "0":
                    Console.Clear();
                    closeManageStacks = true;
                    UserOptions();
                    break;
                case "1":
                    stackService.ViewAllStacks();
                    break;
                case "2":
                    stackService.CreateStack();
                    break;
                case "3":
                    stackService.UpdateStackById();
                    break;
                case "4":
                    stackService.DeleteStackById();
                    break;
                default:
                    Console.WriteLine("\nYou entered an invalid option, please try again.");
                    break;
            }
        }
    }

    private static void ManageFlashcards()
    {
        FlashcardService flashcardService = new FlashcardService(new Repository.FlashcardRepository.FlashcardRepository());
        bool closeManageFlashcards = false;

        while (closeManageFlashcards == false)
        {
            Console.Clear();
            Console.WriteLine("Manage Flashcards Menu:");   
            Console.WriteLine("<---------------------------------------------------------->\n");
            Console.WriteLine("Type 0 to Return to Main Menu.");
            Console.WriteLine("Type 1 to View All Flashcards");
            Console.WriteLine("Type 2 to Create a New Flashcard");
            Console.WriteLine("Type 3 to Edit a Flashcard");
            Console.WriteLine("Type 4 to Delete a Flashcard");
            Console.WriteLine("\n<---------------------------------------------------------->");

            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "0":
                    Console.Clear();
                    closeManageFlashcards = true;
                    UserOptions();
                    break;
                 case "1":
                    flashcardService.ViewAllFlashcards();
                     break;
                case "2":
                    flashcardService.CreateFlashcard();
                    break;
                case "3":
                    flashcardService.UpdateFlashcardById();
                    break;
                case "4":
                    flashcardService.DeleteFlashcardById();
                    break;
                default:
                    Console.WriteLine("\nYou entered an invalid option, please try again.");
                    break;
            }
        }
    }
}