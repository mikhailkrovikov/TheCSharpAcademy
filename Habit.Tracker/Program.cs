namespace Habit.Tracker;

public class Program
{
    public static void Main()
    {
        var dataprovider = new DataProvider();
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
                    dataprovider.Create(record);
                }
                else if (action == UserAction.Read)
                {
                    var data = dataprovider.ReadAllData();
                    consoleUI.PrintData(data);
                }
                else if (action == UserAction.Update)
                {
                    var input = consoleUI.GetIdInput();
                    var record = consoleUI.GetRecordFromUser();
                    dataprovider.Update(input, record);
                }
                else if (action == UserAction.Delete)
                {

                    var input = consoleUI.GetIdInput();
                    dataprovider.Delete(input);
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
