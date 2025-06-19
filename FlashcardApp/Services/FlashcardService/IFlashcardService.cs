namespace FlashcardApp.Services.FlashcardService;

public interface IFlashcardService
{
    public void ViewAllFlashcards();
    public void CreateFlashcard();
    public void UpdateFlashcardById();
    public void DeleteFlashcardById();
}