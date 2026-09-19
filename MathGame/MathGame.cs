using System.Diagnostics;

namespace MathGame;

public class MathGame
{
    private readonly IUIActions actions;
    private readonly Stopwatch stopwatch;
    private static readonly List<int> scoreHistory = new();
    private const int NumberOfQuestions = 5;

    public MathGame(IUIActions actions)
    {
        this.actions = actions;
        stopwatch = new();
    }

    public int StartGame()
    {
        var score = 0;
        actions.ShowWelcomeMessage();
        stopwatch.Start();
        for (int i = 0; i < NumberOfQuestions; i++)
        {
            var operation = actions.ReadInput();
            while (!QuestionGenerator.IsOperationValid(operation))
            {
                actions.ShowInvalidOperationMessage();
                operation = actions.ReadInput();
            }
            var (firstNumber, secondNumber) = QuestionGenerator.GenerateNumbers(operation);
            var expected = Calculator.Calculate(firstNumber, secondNumber, operation);
            actions.ShowQuestion(firstNumber, secondNumber, operation);
            if (!int.TryParse(actions.ReadInput(), out var userAnswer))
            {
                actions.ShowInvalidOperationMessage();
                continue;
            }
            if (userAnswer == expected)
            {
                score++;
                actions.ShowCorrectAnswerMessage(score);
            }
            else
            {
                actions.ShowIncorrectAnswerMessage(expected, score);
            }
        }
        stopwatch.Stop();
        scoreHistory.Add(score);
        actions.ShowFinalScore(score, stopwatch.ElapsedMilliseconds);
        stopwatch.Reset();
        return score;
    }

    public void ShowScoreHistory()
    {
        actions.ShowScoreHistory(scoreHistory);
    }
}
