namespace FlashcardApp.Services.StackService;

public interface IStackService
{
    public void ViewAllStacks();
    public void CreateStack();
    public void UpdateStackById();
    public void DeleteStackById();
}