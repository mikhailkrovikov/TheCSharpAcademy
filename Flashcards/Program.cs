using Spectre.Console;

namespace Flashcards
{
    public enum MainUserAction
    {
        ManageStacks,
        ManageFlashcards,
        Study,
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
        public static void GetAction()
        {
            var result = AnsiConsole
                .Prompt(new SelectionPrompt<string>()
                    .Title("Choose action")
                    .AddChoices("Manage stacks"));
        }
    }

}
