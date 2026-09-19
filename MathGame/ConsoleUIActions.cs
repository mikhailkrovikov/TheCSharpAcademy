namespace MathGame;

public class ConsoleUIActions : IUIActions
{
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
        Console.WriteLine($"Your score is: {score}\n" +
                          $"Time is: {elappsedMilliseconds / 1000} sec\n" +
                          $"To see score print S\n" +
                          $"To start new game, press any key...");
    }

    public void ShowIncorrectAnswerMessage(int expected, int score)
    {
        Console.WriteLine($"Incorrect! Right answer is {expected}. Score is {score}");
    }

    public void ShowQuestion(int firstNumber, int secondNumber, string operation)
    {
        Console.WriteLine($"{firstNumber} {operation} {secondNumber} = ?");
    }

    public void ShowScoreHistory(IEnumerable<int> scoreHistory)
    {
        Console.Clear();
        foreach (var scoreItem in scoreHistory)
        {
            Console.WriteLine(scoreItem);
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    public void ShowWelcomeMessage()
    {
        Console.WriteLine("Welcome to the Math Game!");
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
