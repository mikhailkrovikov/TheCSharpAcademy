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
        var dataprovider = new DataProvider();
        dataprovider.CreateDatabase();
        while (true)
        {
            try
            {
                var action = GetUserAction();

                if (action == UserAction.Create)
                {
                    var record = GetRecordFromUser();
                    dataprovider.Create(record);
                }
                else if (action == UserAction.Read)
                {
                    foreach (var item in dataprovider.ReadAllData())
                    {
                        Console.WriteLine(item);
                    }
                }
                else if (action == UserAction.Update)
                {
                    var input = GetIntInput();
                    var record = GetRecordFromUser();
                    dataprovider.Update(input, record);
                }
                else if (action == UserAction.Delete)
                {

                    var input = GetIntInput();
                    dataprovider.Delete(input);
                }
                else if (action == UserAction.Exit)
                {
                    break;
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
        Console.WriteLine("Type C to create new record");
        Console.WriteLine("Type R to read all the records");
        Console.WriteLine("Type U to update record");
        Console.WriteLine("Type D to delete record");
        Console.WriteLine("Type E to exit the application");
    }

    private static int GetIntInput()
    {
        Console.WriteLine("Enter id of removing record");
        var input = Convert.ToInt32(Console.ReadLine());
        return input;
    }

    private static UserAction GetUserAction()
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
