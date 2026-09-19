using CalculatorLibrary;
using System.Text.RegularExpressions;

namespace CalculatorProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            Calculator calculator = new Calculator();
            while (!endApp)
            {
                // Declare variables and set to empty.
                // Use Nullable types (with ?) to match type of System.Console.ReadLine
                string? numInput1 = "";
                string? numInput2 = "";
                double result = 0;

                // Ask the user to type the first number.
                Console.Write("Type a number, type 'h' to use a result from history, and then press Enter: ");
                numInput1 = Console.ReadLine();

                double cleanNum1 = 0;
                bool firstNumberEntered = false;
                while (!firstNumberEntered)
                {
                    if (numInput1 == "h")
                    {
                        IReadOnlyList<string> history = calculator.GetHistory();

                        if (history.Count == 0)
                        {
                            Console.WriteLine("The calculation history is empty.");
                            Console.Write("Type a number, and then press Enter: ");
                            numInput1 = Console.ReadLine();
                            continue;
                        }

                        Console.WriteLine("Calculation history:");
                        for (int i = 0; i < history.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {history[i]}");
                        }

                        Console.Write("Choose a calculation number: ");
                        string? historyInput = Console.ReadLine();

                        if (int.TryParse(historyInput, out int historyIndex)
                            && historyIndex > 0
                            && historyIndex <= history.Count)
                        {
                            cleanNum1 = calculator.GetHistoryResult(historyIndex - 1);
                            firstNumberEntered = true;
                        }
                        else
                        {
                            Console.WriteLine("This is not a valid history item.");
                            Console.Write("Type a number, type 'h' to use a result from history, and then press Enter: ");
                            numInput1 = Console.ReadLine();
                        }
                    }
                    else if (double.TryParse(numInput1, out cleanNum1))
                    {
                        firstNumberEntered = true;
                    }
                    else
                    {
                        Console.Write("This is not valid input. Please enter a number or type 'h' to use a result from history: ");
                        numInput1 = Console.ReadLine();
                    }
                }

                // Ask the user to choose an operator.
                Console.WriteLine("Choose an operator from the following list:");
                Console.WriteLine("\ta - Add");
                Console.WriteLine("\ts - Subtract");
                Console.WriteLine("\tm - Multiply");
                Console.WriteLine("\td - Divide");
                Console.WriteLine("\tr - Square root");
                Console.WriteLine("\tp - Power");
                Console.WriteLine("\tx - 10^x");
                Console.WriteLine("\tsin - Sine");
                Console.WriteLine("\tcos - Cosine");
                Console.WriteLine("\ttan - Tangent");
                Console.Write("Your option? ");

                string? op = Console.ReadLine();

                // Validate input is not null, and matches the pattern
                if (op == null || !Regex.IsMatch(op, "^(a|s|m|d|r|p|x|sin|cos|tan)$"))
                {
                    Console.WriteLine("Error: Unrecognized input.");
                }
                else
                {
                    double cleanNum2 = 0;

                    // Ask the user to type the second number for operations that need two operands.
                    if (op == "a" || op == "s" || op == "m" || op == "d" || op == "p")
                    {
                        Console.Write("Type another number, type 'h' to use a result from history, and then press Enter: ");
                        numInput2 = Console.ReadLine();

                        bool secondNumberEntered = false;
                        while (!secondNumberEntered)
                        {
                            if (numInput2 == "h")
                            {
                                IReadOnlyList<string> history = calculator.GetHistory();

                                if (history.Count == 0)
                                {
                                    Console.WriteLine("The calculation history is empty.");
                                    Console.Write("Type another number, and then press Enter: ");
                                    numInput2 = Console.ReadLine();
                                    continue;
                                }

                                Console.WriteLine("Calculation history:");
                                for (int i = 0; i < history.Count; i++)
                                {
                                    Console.WriteLine($"{i + 1}. {history[i]}");
                                }

                                Console.Write("Choose a calculation number: ");
                                string? historyInput = Console.ReadLine();

                                if (int.TryParse(historyInput, out int historyIndex)
                                    && historyIndex > 0
                                    && historyIndex <= history.Count)
                                {
                                    cleanNum2 = calculator.GetHistoryResult(historyIndex - 1);
                                    secondNumberEntered = true;
                                }
                                else
                                {
                                    Console.WriteLine("This is not a valid history item.");
                                    Console.Write("Type another number, type 'h' to use a result from history, and then press Enter: ");
                                    numInput2 = Console.ReadLine();
                                }
                            }
                            else if (double.TryParse(numInput2, out cleanNum2))
                            {
                                secondNumberEntered = true;
                            }
                            else
                            {
                                Console.Write("This is not valid input. Please enter a number or type 'h' to use a result from history: ");
                                numInput2 = Console.ReadLine();
                            }
                        }
                    }

                    try
                    {
                        result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                        if (double.IsNaN(result))
                        {
                            Console.WriteLine("This operation will result in a mathematical error.\n");
                        }
                        else Console.WriteLine("Your result: {0:0.##}\n", result);

                        Console.WriteLine($"Calculator used {calculator.GetUsageCount()} times.\n");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                    }
                }
                Console.WriteLine("------------------------\n");

                // Wait for the user to respond before closing.
                Console.Write("Press 'n' and Enter to close the app, 'h' to show history, 'c' to clear history, or press any other key and Enter to continue: ");
                string? userOption = Console.ReadLine();

                if (userOption == "n")
                {
                    endApp = true;
                }
                else if (userOption == "h")
                {
                    IReadOnlyList<string> history = calculator.GetHistory();

                    if (history.Count == 0)
                    {
                        Console.WriteLine("The calculation history is empty.");
                    }
                    else
                    {
                        Console.WriteLine("Calculation history:");
                        for (int i = 0; i < history.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {history[i]}");
                        }
                    }
                }
                else if (userOption == "c")
                {
                    calculator.ClearHistory();
                    Console.WriteLine("Calculation history cleared.");
                }

                Console.WriteLine("\n"); // Friendly linespacing.
            }
            // Add call to close the JSON writer before return
            calculator.Finish();
            return;
        }
    }
}
