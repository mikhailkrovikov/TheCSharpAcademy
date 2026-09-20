using Microsoft.Data.Sqlite;

namespace Habit.Tracker;

public enum UserAction
{
    Create,
    Read,
    Update,
    Delete,
    Exit,
}

public class Program
{
    public static void Main()
    {
        GoToMainMenu();
        var exit = false;
        DataProvider.CreateDatabase();
        while (true)
        {
            try
            {
                var action = GetUserAction();
                if (action == UserAction.Read)
                {
                    foreach (var item in DataProvider.GetAllData())
                    {
                        Console.WriteLine(item);
                    }
                }
                else if (action == UserAction.Create)
                {
                    var record = GetRecordFromUser();
                    DataProvider.AddDate(record);
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                GoToMainMenu();
            }
            
        }

    }

    private static void GoToMainMenu()
    {
        Console.WriteLine("\nMAIN MENU");
        Console.WriteLine("Choose action:");
        Console.WriteLine("Type R to read all the records");
        Console.WriteLine("Type C to create new record");
        Console.WriteLine("Type E to exit the application");
    }

    private static UserAction GetUserAction()
    {
        var choice = Console.ReadLine();
        if (choice == "R")
        {
            return UserAction.Read;
        }
        else if (choice == "C")
        {
            return UserAction.Create;
        }
        throw new ArgumentException("Invalid command");
    }

    private static Record GetRecordFromUser()
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
