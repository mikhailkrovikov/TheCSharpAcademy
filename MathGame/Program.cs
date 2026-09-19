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
            game.StartGame();
            if (consoleUi.ReadInput() == "S")
            {
                game.ShowScoreHistory();
            }
            consoleUi.ClearScreen();
        }
    }
}