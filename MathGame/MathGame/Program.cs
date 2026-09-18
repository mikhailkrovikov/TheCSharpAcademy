namespace MathGame;

public class Program
{
    private static List<int> _scoreHistory = new List<int>();
    private const int NumberOfQuestions = 2;

    public static void Main()
    {
        while (true)
        {

            var score = StartGame();
            _scoreHistory.Add(score);
            if (Console.ReadLine() == "S")
            {
                ShowScoreHistory();
            }
            Console.Clear();
        }   
    }

    private static void ShowScoreHistory()
    {
        Console.Clear();
        for(var i = 0; i < _scoreHistory.Count; i++)
        {
            Console.WriteLine($"Game {i + 1}: {_scoreHistory[i]}");
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private static int StartGame()
    {
        var score = 0;
        Console.WriteLine("Welcome to the Math Game!");
        Console.WriteLine("Enter operation: -, +, *, /");
        for (int i = 0; i < NumberOfQuestions; i++)
        {
            var operation = Console.ReadLine();
            while (!IsOperationValid(operation))
            {
                Console.WriteLine("Invalid operation!");
                operation = Console.ReadLine();
            }

            var firstNumber = GetRandomNumber(0, 100);
            var secondNumber = GetRandomNumber(0, 100);
            var expected = Calculate(firstNumber, secondNumber, operation);

            if (!int.TryParse(Console.ReadLine(), out var userAnswer))
            {
                Console.WriteLine("Invalid input!");
                continue;
            }

            if (userAnswer == expected)
            {
                score++;
                Console.WriteLine($"Correct! Score is {score}. Enter next operation");
            }
            else
            {
                Console.WriteLine($"Incorrect! Right answer is {expected}. Score is {score}");
            }
        }
        Console.WriteLine($"Your score is: {score}\n" +
            $"To see score print S\n" +
            $"To start new game, press any key...");
        return score;
    }

    private static int Calculate(int firstNumber, int secondNumber, string operation)
    {
        var expected = 0;
        switch (operation)
        {
            case "+":
                Console.WriteLine($"{firstNumber} + {secondNumber} = ?");
                expected = firstNumber + secondNumber;
                break;
            case "-":
                secondNumber = GetRandomNumber(0, firstNumber);
                Console.WriteLine($"{firstNumber} - {secondNumber} = ?");
                expected = firstNumber - secondNumber;
                break;
            case "*":
                Console.WriteLine($"{firstNumber} * {secondNumber} = ?");
                expected = firstNumber * secondNumber;
                break;
            case "/":
                secondNumber = GetRandomNumberForDivision(firstNumber);
                Console.WriteLine($"{firstNumber} / {secondNumber} = ?");
                expected = firstNumber / secondNumber;
                break;
        }
        return expected;
    }

    private static int GetRandomNumber(int min, int max)
    {
        return new Random().Next(min, max);
    }

    private static int GetRandomNumberForDivision(int firstNumber)
    {
        var rnd = GetRandomNumber(1, firstNumber);
        while (firstNumber % rnd != 0 || rnd == 0)
        {
            rnd = GetRandomNumber(1, firstNumber);
        }
        return rnd;
    }

    private static bool IsOperationValid(string operation)
    {
        return operation == "+" || operation == "-" || operation == "*" || operation == "/";
    }
}
