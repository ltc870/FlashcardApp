namespace FlashcardApp.Repository.StackRepository;

public interface IStackRepository
{
    public void ViewAllStacks();
    public void CreateStack(string stackName);
    public void UpdateStackById(int stackId, string stackName);
    public void DeleteStackById(int stackId);
}