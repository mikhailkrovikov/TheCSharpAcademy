namespace MathGame;

public static class Program
{
    public static void Main()
    {
        var consoleUi = new ConsoleUIActions();
        ConsoleUIActions.SetupConsole(ConsoleColor.DarkBlue, ConsoleColor.Yellow);
        var game = new MathGame(consoleUi);
        while (true)
        {
            var choice = consoleUi.MakeChoise();
            if (choice == Choice.Score)
            {
                game.ShowScoreHistory();
            }
            if (choice == Choice.Game)
            {
                game.StartGame();
            }
            consoleUi.ClearScreen();
        }
    }
}