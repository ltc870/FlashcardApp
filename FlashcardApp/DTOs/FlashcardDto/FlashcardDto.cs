namespace FlashcardApp.DTOs.FlashcardDTO;

public record FlashcardDto
(
    int FlashcardId, 
    string? FrontText,
    string? BackText,
    string? StackName
);