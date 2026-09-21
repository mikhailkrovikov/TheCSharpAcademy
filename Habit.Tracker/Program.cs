namespace Habit.Tracker;

public class Program
{
    public static void Main()
    {
        var dataprovider = new SQLiteDataProvider();
        var consoleUI = new ConsoleUserInterface();

        dataprovider.CreateDatabase();
        consoleUI.GoToMainMenu();

        while (true)
        {
            try
            {
                var action = consoleUI.GetUserAction();
                var result = false;
                switch (action)
                {
                    case UserAction.Create:
                        {
                            var record = consoleUI.GetRecordFromUser();
                            result = dataprovider.Create(record);
                            break;
                        }

                    case UserAction.Read:
                        {
                            var data = dataprovider.ReadAllData();
                            consoleUI.PrintData(data);
                            result = data.Count > 0;
                            break;
                        }

                    case UserAction.Update:
                        {
                            var data = dataprovider.ReadAllData();
                            consoleUI.PrintData(data);
                            var input = consoleUI.GetIdInput();
                            var record = consoleUI.GetRecordFromUser();
                            result = dataprovider.Update(input, record);
                            break;
                        }

                    case UserAction.Delete:
                        {
                            var data = dataprovider.ReadAllData();
                            consoleUI.PrintData(data);
                            var input = consoleUI.GetIdInput();
                            result = dataprovider.Delete(input);
                            break;
                        }
                }
                if (action == UserAction.Exit)
                {
                    break;
                }
                consoleUI.PrintEndMessage(action, result);
            }
            catch (ArgumentException ex)
            {
                consoleUI.PrintError(ex.Message);
                consoleUI.GoToMainMenu();
            }
        }
    }
}
