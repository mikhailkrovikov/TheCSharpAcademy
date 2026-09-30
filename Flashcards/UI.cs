using Flashcards.DTOs;
using Spectre.Console;

namespace Flashcards
{
    public static class UI
    {
        public static void PrintMessage(string message)
        {
            AnsiConsole.MarkupLine(message);
        }

        public static string GetActions(List<string> actions)
        {
            var str = AnsiConsole
                .Prompt(new SelectionPrompt<string>()
                    .Title("Choose action")
                    .AddChoices(actions));
            return str;
        }

        public static void PrintStackTable(List<CardStackDTO> stacks)
        {
            var table = new Table()
                .AddColumn("Name");
            foreach (var stack in stacks)
                table.AddRow($"{stack.Name}");
            AnsiConsole.Write(table);
        }

        public static void PrintFlashcardTable(List<GetFlashcardDTO> flashcards)
        {
            var number = 1;
            var table = new Table()
                .AddColumn("Id")
                .AddColumn("Front")
                .AddColumn("Back");
            foreach (var flashcard in flashcards)
            {
                table.AddRow(number.ToString(), flashcard.Front, flashcard.Back);
                number++;
            }
            AnsiConsole.Write(table);
        }

        public static List<int> PrintStackChoices(List<CardStackDTO> stacks)
        {
            if (stacks.Count == 0)
            {
                AnsiConsole.MarkupLine("No stacks yet;");
                return null;
            }

            var multiPrompt = new MultiSelectionPrompt<string>()
                .Title("Choose stack")
                .NotRequired()
                .InstructionsText("[grey](Press [blue]<space>[/] to toggle, [green]<enter>[/] to confirm)[/]");

            foreach (var stack in stacks)
            {
                multiPrompt.AddChoice(stack.Name);
            }

            var choices = AnsiConsole.Prompt(multiPrompt);
            return stacks
                .Where(s => choices.Contains(s.Name))
                .Select(s => s.Id)
                .ToList();
        }

        public static List<int> PrintFlashcardChoices(List<GetFlashcardDTO> flashcards)
        {
            if (flashcards.Count == 0)
            {
                AnsiConsole.MarkupLine("No flashcards yet;");
                return new List<int>();
            }

            var multiPrompt = new MultiSelectionPrompt<GetFlashcardDTO>()
                .Title("Choose flashcards")
                .UseConverter(f => Markup.Escape(f.Front + " - " + f.Back))
                .NotRequired()
                .InstructionsText("[grey](Press [blue]<space>[/] to toggle, [green]<enter>[/] to confirm)[/]");

            foreach (var flashcard in flashcards)
            {
                multiPrompt.AddChoice(flashcard);
            }

            var choices = AnsiConsole.Prompt(multiPrompt);
            return choices
                .Select(s => s.Id)
                .ToList();
        }
    }
}
