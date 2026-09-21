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
        if (userAction == UserAction.Create)
        {
            if (succes)
            {
                Console.WriteLine("Record was succesfully added");
                Console.WriteLine("Press next command to do");
            }
            else
            {
                Console.WriteLine("An error occures with add");
                Console.WriteLine("Press next command to do");
            }
        }
        else if (userAction == UserAction.Read)
        {
            if (succes)
            {
                Console.WriteLine("Press next command to do\n");
            }
            else
            {
                Console.WriteLine("An error occures with read");
                Console.WriteLine("Press next command to do");
            }
        }
        else if (userAction == UserAction.Delete)
        {
            if (succes)
            {
                Console.WriteLine("Record was succesfully deleted");
                Console.WriteLine("Press next command to do\n");
            }
            else
            {
                Console.WriteLine("An error occures with delete");
                Console.WriteLine("Press next command to do");
            }
        }
        else if (userAction == UserAction.Update)
        {
            if (succes)
            {
                Console.WriteLine("Record was succesfully updated");
                Console.WriteLine("Press next command to do\n");
            }
            else
            {
                Console.WriteLine("An error occures with update");
                Console.WriteLine("Press next command to do");
            }
        }
        else throw new ArgumentException($"Invalid action {userAction}");
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
        else throw new ArgumentException("Invalid command");
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
