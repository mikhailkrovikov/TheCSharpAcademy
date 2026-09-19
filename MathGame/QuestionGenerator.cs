namespace MathGame;

public static class QuestionGenerator
{
    private static readonly Random random = new();

    /// <summary>
    /// Tuple needs for correct handle of random numbers in case of operations of substraction and division
    /// </summary>
    /// <param name="operation"></param>
    /// <returns></returns>
    public static (int FirstNumber, int SecondNumber) GenerateNumbers(string operation)
    {
        var firstNumber = random.Next(0, 100);
        int secondNumber;
        switch (operation)
        {
            case "-":
                secondNumber = random.Next(0, firstNumber + 1);
                break;

            case "/":
                firstNumber = random.Next(1, 100);
                secondNumber = GetDivisor(firstNumber);
                break;

            default:
                secondNumber = random.Next(0, 100);
                break;
        }
        return (firstNumber, secondNumber);
    }


    private static int GetDivisor(int number)
    {
        var divisor = random.Next(1, number + 1);
        while (number % divisor != 0)
        {
            divisor = random.Next(1, number + 1);
        }
        return divisor;
    }

    public static bool IsOperationValid(string operation)
    {
        return
            operation == "+" ||
            operation == "-" ||
            operation == "*" ||
            operation == "/";
    }
}
