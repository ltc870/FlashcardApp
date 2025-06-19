namespace FlashcardApp.Models.Flashcard;

public class Flashcard
{
    public int FlashcardId { get; set; }
    public string? FrontText { get; set; }
    public string? BackText { get; set; }
    public int StackId { get; set; }
    public string? StackName { get; set; }
}