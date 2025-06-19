using FlashcardApp.Utils;

namespace FlashcardApp;

class Program
{
    static void Main(string[] args)
    {
        TableInsertUtil.CreateTables();
        
        RunProgramUtil.StartProgram();
    }
}