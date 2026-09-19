namespace MathGame;

public interface IUIActions
{
    void ShowScoreHistory(IEnumerable<int> scoreHistory);
    void ShowWelcomeMessage();
    void ShowQuestion(int firstNumber, int secondNumber, string operation);
    void ShowCorrectAnswerMessage(int score);
    void ShowIncorrectAnswerMessage(int expected, int score);
    void ShowFinalScore(int score, long ellapsedMilliseconds);
    void ShowInvalidOperationMessage();
    string? ReadInput();
    void ClearScreen();
}
