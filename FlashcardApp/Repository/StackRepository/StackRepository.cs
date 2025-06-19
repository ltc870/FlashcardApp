using FlashcardApp.Models.Stack;
using Microsoft.Data.SqlClient;


namespace FlashcardApp.Repository.StackRepository;

public class StackRepository : IStackRepository
{
    public void ViewAllStacks()
    {
        List<Stack> stackData = new List<Stack>();
        using var connection = DbHelper.DbHelper.GetConnection();
        connection.Open();
        var cmd = new SqlCommand($"SELECT * FROM Stacks", connection);
        try
        {
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    stackData.Add(
                        new Stack
                        {
                            StackId = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        }
                    );
                }
            }
            else
            {
                Console.WriteLine("No stacks found.");
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"An error occurred while fetching stacks: {ex.Message}");
        }
        finally
        {
            connection.Close();
        }
        
        stackData = new List<Stack>(stackData.OrderBy(s => s.StackId));
        
        Console.WriteLine("<------------------------------------------>\n");
        foreach (Stack stack in stackData)
        {
            Console.WriteLine($"{stack.StackId}. {stack.Name}");
        }
        Console.WriteLine("\n<------------------------------------------>");
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }
    
    public void CreateStack(string stackName)
    {
        Stack stack = new Stack();
        stack.Name = stackName;
        
        using var connection = DbHelper.DbHelper.GetConnection();
        connection.Open();
        var cmd = new SqlCommand("INSERT INTO Stacks (Name) VALUES (@Name)", connection);
        cmd.Parameters.AddWithValue("@Name", stack.Name);
        try
        {
            cmd.ExecuteNonQuery();
            Console.WriteLine($"Stack '{stack.Name}' created successfully.");
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"An error occurred while creating the stack: {ex.Message}");
        }
        finally
        {
            connection.Close();
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }

    public void UpdateStackById(int newStackId, string newStackName)
    {
        Stack stack = new Stack
        {
            StackId = newStackId,
            Name = newStackName
        };
        
        using var connection = DbHelper.DbHelper.GetConnection();
        connection.Open();
        var updateCmd = new SqlCommand("UPDATE Stacks " +
                                       "SET Name = @Name " +
                                       "WHERE StackId = @StackId", connection);
        updateCmd.Parameters.AddWithValue("@Name", stack.Name);
        updateCmd.Parameters.AddWithValue("@StackId", stack.StackId);

        try
        {
            updateCmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Could not update stack name: {e}");
            throw;
        }
        Console.WriteLine($"Stack with ID {stack.StackId} updated to '{stack.Name}' successfully.");
        connection.Close();
        
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }

    public void DeleteStackById(int stackId)
    {
        using var connection = DbHelper.DbHelper.GetConnection();
        connection.Open();
        var deleteCmd = new SqlCommand($"DELETE FROM Stacks Where StackId = @StackId", connection);
        deleteCmd.Parameters.AddWithValue("@StackId", stackId);

        try
        {
            deleteCmd.ExecuteNonQuery();
            Console.WriteLine($"Stack with ID {stackId} deleted successfully.");
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Could not delete stack: {e}");
            throw;
        }
        
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }
}