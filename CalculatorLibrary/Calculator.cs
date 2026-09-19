using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {

        JsonWriter writer;
        int usageCount = 0;
        List<string> history = new List<string>();
        List<double> historyResults = new List<double>();

        public Calculator()
        {
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }

        // CalculatorLibrary.cs
        public double DoOperation(double num1, double num2, string op)
        {
            double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
            string calculation = "";
            usageCount++;

            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);

            if (op == "a" || op == "s" || op == "m" || op == "d" || op == "p")
            {
                writer.WritePropertyName("Operand2");
                writer.WriteValue(num2);
            }

            writer.WritePropertyName("Operation");
            // Use a switch statement to do the math.
            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    writer.WriteValue("Add");
                    calculation = $"{num1} + {num2} = {result}";
                    break;
                case "s":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    calculation = $"{num1} - {num2} = {result}";
                    break;
                case "m":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    calculation = $"{num1} * {num2} = {result}";
                    break;
                case "d":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        calculation = $"{num1} / {num2} = {result}";
                    }
                    writer.WriteValue("Divide");
                    break;
                case "r":
                    result = Math.Sqrt(num1);
                    writer.WriteValue("Square root");
                    calculation = $"sqrt({num1}) = {result}";
                    break;
                case "p":
                    result = Math.Pow(num1, num2);
                    writer.WriteValue("Power");
                    calculation = $"{num1}^{num2} = {result}";
                    break;
                case "x":
                    result = Math.Pow(10, num1);
                    writer.WriteValue("10^x");
                    calculation = $"10^{num1} = {result}";
                    break;
                case "sin":
                    result = Math.Sin(num1);
                    writer.WriteValue("Sine");
                    calculation = $"sin({num1}) = {result}";
                    break;
                case "cos":
                    result = Math.Cos(num1);
                    writer.WriteValue("Cosine");
                    calculation = $"cos({num1}) = {result}";
                    break;
                case "tan":
                    result = Math.Tan(num1);
                    writer.WriteValue("Tangent");
                    calculation = $"tan({num1}) = {result}";
                    break;
                // Return text for an incorrect option entry.
                default:
                    break;
            }

            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            if (!double.IsNaN(result))
            {
                history.Add(calculation);
                historyResults.Add(result);
            }

            return result;
        }

        public int GetUsageCount()
        {
            return usageCount;
        }

        public IReadOnlyList<string> GetHistory()
        {
            return history.AsReadOnly();
        }

        public double GetHistoryResult(int index)
        {
            return historyResults[index];
        }

        public void ClearHistory()
        {
            history.Clear();
            historyResults.Clear();
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }
    }
}
