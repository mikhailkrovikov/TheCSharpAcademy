namespace Habit.Tracker;

public class ConsoleUserInterface : IUserInterface
{
    public void GoToMainMenu()
    {
        Console.WriteLine("\nChoose action:");
        Console.WriteLine("Type C to create new record");
        Console.WriteLine("Type R to read all the records");
        Console.WriteLine("Type U to update record");
        Console.WriteLine("Type D to delete record");
        Console.WriteLine("Type E to exit the application\n");
    }
    public void PrintData(List<Record> list)
    {
        foreach (var item in list)
        {
            Console.WriteLine(item.ToString());
        }
    }

    public void PrintEndMessage(UserAction userAction, bool succes)
    {
        switch (userAction)
        {
            case UserAction.Create:
                if (succes)
                {
                    Console.WriteLine("Record was succesfully added");
                    GoToMainMenu();
                }
                else
                {
                    Console.WriteLine("An error occures with add");
                    GoToMainMenu();
                }
                break;
            case UserAction.Read:
                if (succes)
                {
                    GoToMainMenu();
                }
                else
                {
                    Console.WriteLine("An error occures with read");
                    GoToMainMenu();
                }
                break;
            case UserAction.Delete:
                if (succes)
                {
                    Console.WriteLine("Record was succesfully deleted");
                    GoToMainMenu();
                }
                else
                {
                    Console.WriteLine("An error occures with delete");
                    GoToMainMenu();
                }
                break;
            case UserAction.Update:
                if (succes)
                {
                    Console.WriteLine("Record was succesfully updated");
                    GoToMainMenu(); ;
                }
                else
                {
                    Console.WriteLine("An error occures with update"); 
                    GoToMainMenu();
                }
                break;
            default:
                throw new ArgumentException($"Invalid action {userAction}");
        }
    }

    public UserAction GetUserAction()
    {
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "C":
            case "c":
                return UserAction.Create;
            case "R":
            case "r":
                return UserAction.Read;
            case "U":
            case "u":
                return UserAction.Update;
            case "D":
            case "d":
                return UserAction.Delete;
            case "E":
            case "e":
                return UserAction.Exit;
            default:
                throw new ArgumentException("Invalid command");
        }
    }

    public int GetIdInput()
    {
        Console.WriteLine("Enter id of record");
        var input = Console.ReadLine();
        return GetValidInt(input);
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
        var dateTime = GatValidDateTime(inputDate);

        Console.WriteLine("Enter count of record");
        var inputCount = Console.ReadLine();
        var count = GetValidInt(inputCount);

        return new Record
        {
            DateTime = dateTime,
            Count = count
        };
    }

    private static int GetValidInt(string? input)
    {
        var validCount = int.TryParse(input, out int count);
        if (!validCount)
        {
            throw new ArgumentException("Invalid count input");
        }
        return count;
    }

    private static DateTime GatValidDateTime(string? input)
    {
        var validDate = DateTime.TryParse(input, out DateTime dateTime);
        if (!validDate)
        {
            throw new ArgumentException("Invalid input date");
        }
        return dateTime;
    }
}
