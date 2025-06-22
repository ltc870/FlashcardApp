namespace FlashcardApp.DTOs.StudySessionDto;

public record StudySessionDto(
    int StudySessionId, 
    DateTime Date, 
    int Score, 
    string? StackName 
    );