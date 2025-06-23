using FlashcardApp.DTOs.StudySessionDto;
using FlashcardApp.Models.Flashcard;
using FlashcardApp.Models.StudySession;
using FlashcardApp.Repository.StudySessionRepository;
using FlashcardApp.Services.StackService;
using FlashcardApp.Utils;

namespace FlashcardApp.Services.StudySessionService;

public class StudySessionService : IStudySessionService
{

    private readonly IStudySessionRepository _studySessionRepository;
    private readonly IStackService _stackService;
    
    public StudySessionService(IStudySessionRepository studySessionRepository, IStackService stackService)
    {
        _studySessionRepository = studySessionRepository;
        _stackService = stackService;
    }
    
    public void RunStudySession()
    {
        _stackService.ViewAllStacks();
        
        Console.WriteLine("Please select a stack to study from by entering the stack ID or enter 0 to cancel operation:");
        string? stackIdInput;
        int stackIdValue;

        do
        {
            stackIdInput = Console.ReadLine()?.Trim();
            
            if (stackIdInput == "0")
            {
                Console.WriteLine("Canceling operation. Press any key to continue...");
                Console.ReadKey();
                return;
            }
            
            if (string.IsNullOrEmpty(stackIdInput) || !int.TryParse(stackIdInput, out stackIdValue))
            {
                Console.WriteLine("Invalid input. Please enter a valid stack ID.");
            }
        } while (string.IsNullOrEmpty(stackIdInput) || !int.TryParse(stackIdInput, out stackIdValue));
        
        List<Flashcard> flashcards = _studySessionRepository.GetFlashcardsForStudySession(stackIdValue);

        var stackName = flashcards.FirstOrDefault()?.StackName;
        
        Console.WriteLine($"Starting study session for stack: {stackName}");

        bool continueStudySession = true;
        int score = 0;

        while (continueStudySession)
        {
            for (int i = 0; i < flashcards.Count; i++)
            {
                Console.Clear();
                Console.WriteLine("Front Text:");
                Console.WriteLine(flashcards[i].FrontText);
                Console.WriteLine("\nEnter back text or press 0 to exit the session...");
                string? userInput = Console.ReadLine()?.Trim();
                if (userInput == "0")
                {
                    Console.WriteLine("Exiting the study session...");
                    Console.WriteLine($"Your final score is {score} out of {flashcards.Count}");
                    Console.WriteLine("Press any key to exit...");
                    Console.ReadLine();
                    continueStudySession = false;
                    break;
                }

                if (userInput == flashcards[i].BackText)
                {
                    Console.WriteLine("Correct!");
                    score++;
                    Console.WriteLine($"Your score is {score} out of {flashcards.Count}");
                    Console.WriteLine("Press any key to continue to the next flashcard...");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine($"Incorrect! The correct answer is: {flashcards[i].BackText}");
                    Console.WriteLine($"Your score is {score} out of {flashcards.Count}");
                    Console.WriteLine("Press any key to continue to the next flashcard...");
                    Console.ReadLine();
                }

                if (i == flashcards.Count - 1)
                {
                    Console.WriteLine($"There are no more flashcards in the stack {stackName}");
                    Console.WriteLine($"Your final score is {score} out of {flashcards.Count}");
                    Console.WriteLine("Press any key to exit the study session...");
                    Console.ReadLine();
                }
                continueStudySession = false;
            }
        }

        StudySession session = new StudySession
        {
            Date = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy")),
            Score = score,
            StackId = stackIdValue
        };
        
        Console.WriteLine($"Saving study session for stack {stackName} with score {score} on {session.Date}");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        _studySessionRepository.SaveStudySession(session);
    }

    public void GetAllStudySessions()
    {
        Console.Clear();
        
        Console.WriteLine("Viewing all study sessions...");
        
        List<StudySession> studySessionData =  _studySessionRepository.GetAllStudySessions();
        
        Console.WriteLine("<------------------------------------------>\n");
        
        foreach (var session in studySessionData)
        {
            StudySessionDto studySession = ModelToDtoMapperUtil.MapStudySessionToDto(session);
            
            Console.WriteLine($"Id: {studySession.StudySessionId} Date: {studySession.Date.ToShortDateString()} Score: {studySession.Score} StackId: {studySession.StackName}");
        }
        
        Console.WriteLine("\n<------------------------------------------>\n");
        
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
    
}