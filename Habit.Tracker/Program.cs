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

                if (action == UserAction.Create)
                {
                    var record = consoleUI.GetRecordFromUser();
                    var res = dataprovider.Create(record);
                    consoleUI.PrintEndMessage(action, res);

                }
                else if (action == UserAction.Read)
                {
                    var data = dataprovider.ReadAllData();
                    consoleUI.PrintData(data);
                    consoleUI.PrintEndMessage(action, data.Count > 0);
                }
                else if (action == UserAction.Update)
                {
                    var input = consoleUI.GetIdInput();
                    var record = consoleUI.GetRecordFromUser();
                    var res = dataprovider.Update(input, record);
                    consoleUI.PrintEndMessage(action, res);
                }
                else if (action == UserAction.Delete)
                {
                    var input = consoleUI.GetIdInput();
                    var res = dataprovider.Delete(input);
                    consoleUI.PrintEndMessage(action, res);
                }
                else if (action == UserAction.Exit)
                {
                    break;
                }
            }
            catch (ArgumentException ex)
            {
                consoleUI.PrintError(ex.Message);
                consoleUI.GoToMainMenu();
            }
        }
    }
}
