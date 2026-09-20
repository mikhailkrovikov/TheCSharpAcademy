namespace Habit.Tracker;

public interface IUserInterface
{
    void GoToMainMenu();
    UserAction GetUserAction();
    int GetIdInput();
    void PrintError(string str);
    Record GetRecordFromUser();
    void PrintData(List<string> list);
}
