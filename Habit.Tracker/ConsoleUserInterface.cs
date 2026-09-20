namespace Habit.Tracker;

public class ConsoleUserInterface : IUserInterface
{
    public void GoToMainMenu()
    {
        Console.WriteLine("Choose action:");
        Console.WriteLine("Type C to create new record");
        Console.WriteLine("Type R to read all the records");
        Console.WriteLine("Type U to update record");
        Console.WriteLine("Type D to delete record");
        Console.WriteLine("Type E to exit the application");
    }
    public void PrintData(List<string> list)
    {
        foreach (var item in list)
        {
            Console.WriteLine(item);
        }
    }

    public UserAction GetUserAction()
    {
        var choice = Console.ReadLine();

        if (choice == "C")
        {
            return UserAction.Create;
        }
        else if (choice == "R")
        {
            return UserAction.Read;
        }
        else if (choice == "U")
        {
            return UserAction.Update;
        }
        else if (choice == "D")
        {
            return UserAction.Delete;
        }
        else if (choice == "E")
        {
            return UserAction.Exit;
        }
        throw new ArgumentException("Invalid command");
    }

    public int GetIdInput()
    {
        Console.WriteLine("Enter id of record");
        var input = Convert.ToInt32(Console.ReadLine());
        return input;
    }

    public void PrintError(string str)
    {
        Console.WriteLine(str);
        Console.WriteLine("Press any key to exit to menu...");
        Console.ReadKey();
    }

    public Record GetRecordFromUser()
    {
        Console.WriteLine("Enter date of record");
        var inputDate = Console.ReadLine();
        var validDate = DateTime.TryParse(inputDate, out DateTime dateTime);
        if (!validDate)
        {
            throw new ArgumentException("Invalid input date");
        }
        Console.WriteLine("Enter count of record");
        var inputCount = Console.ReadLine();
        var validCount = int.TryParse(inputCount, out int count);
        if (!validCount)
        {
            throw new ArgumentException("Invalid count input");
        }
        return new Record
        {
            DateTime = dateTime,
            Count = count
        };
    }
}
