namespace MathGame;

public class ConsoleUIActions : IUIActions
{
    public Choice MakeChoise()
    {
        Console.WriteLine("Welcome to the Math Game!");
        Console.WriteLine("To see your score history press S...");
        Console.WriteLine("To start a game press any other key..");
        var choise = Console.ReadLine();
        if (choise == "S" || choise == "s")
            return Choice.Score;
        return Choice.Game;
    }
    public static void SetupConsole(ConsoleColor background, ConsoleColor foreground)
    {
        Console.BackgroundColor = background;
        Console.Clear();
        Console.ForegroundColor = foreground;
    }

    public void ShowCorrectAnswerMessage(int score)
    {
        Console.WriteLine($"Correct! Score is {score}. Enter next operation");
    }

    public void ShowFinalScore(int score, long elappsedMilliseconds)
    {
        Console.WriteLine($"\nGame over!\n" + 
                          $"Your score is: {score}\n" +
                          $"Time is: {elappsedMilliseconds / 1000} sec\n" +
                          $"To exit to menu, press any key...");
        Console.ReadKey();
    }

    public void ShowIncorrectAnswerMessage(int expected, int score)
    {
        Console.WriteLine($"Incorrect! Right answer is {expected}. Score is {score}. Enter next operation");
    }

    public void ShowQuestion(int firstNumber, int secondNumber, string operation)
    {
        Console.WriteLine($"{firstNumber} {operation} {secondNumber} = ?");
    }

    public void ShowScoreHistory(List<int> scoreHistory)
    {
        Console.Clear();
        if (scoreHistory.Count == 0)
        {
            Console.WriteLine("Your score history is empty");
        }
        else
        {
            Console.WriteLine("Your score history is:");
            for (var i = 0; i < scoreHistory.Count; i++)
            {
                Console.WriteLine($"Game {i + 1} : {scoreHistory[i]} points");
            }
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    public void ShowWelcomeMessage()
    {
        Console.WriteLine("Enter operation: -, +, *, /");
    }

    public string? ReadInput()
    {
        return Console.ReadLine();
    }

    public void ShowInvalidOperationMessage()
    {
        Console.WriteLine("Invalid operation!");
    }

    public void ClearScreen()
    {
        Console.Clear();
    }
}
