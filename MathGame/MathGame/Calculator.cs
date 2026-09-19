namespace MathGame;

public static class Calculator
{
    public static int Calculate(int firstNumber, int secondNumber, string operation)
    {
        var expected = 0;
        switch (operation)
        {
            case "+":
                expected = firstNumber + secondNumber;
                break;
            case "-":
                expected = firstNumber - secondNumber;
                break;
            case "*":
                expected = firstNumber * secondNumber;
                break;
            case "/":
                expected = firstNumber / secondNumber;
                break;
        }
        return expected;
    }
}
