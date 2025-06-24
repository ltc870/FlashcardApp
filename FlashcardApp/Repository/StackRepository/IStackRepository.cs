using FlashcardApp.Models.Stack;

namespace FlashcardApp.Repository.StackRepository;

public interface IStackRepository
{
    public List<Stack> ViewAllStacks();
    public void CreateStack(string stackName);
    public void UpdateStackById(int stackId, string stackName);
    public void DeleteStackById(int stackId);
}