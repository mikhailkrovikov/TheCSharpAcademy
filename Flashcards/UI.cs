using Flashcards.DTOs;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcards
{
    public class UI
    {

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
            var table = new Table()
                .AddColumn("Front")
                .AddColumn("Back");
            foreach (var flashcard in flashcards)
                table.AddRow(flashcard.Front, flashcard.Back);
            AnsiConsole.Write(table);
        }

        public static List<int> PrintStackChoices(List<CardStackDTO> stacks)
        {
            if(stacks.Count == 0) 
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

        public static List<int> PrintFlashcardChoices(List<CreateFlashcardDTO> flashcards)
        {
            if (flashcards.Count == 0)
            {
                AnsiConsole.MarkupLine("No flashcards yet;");
                return null;
            }

            var multiPrompt = new MultiSelectionPrompt<string>()
                .Title("Choose flashcards")
                .NotRequired()
                .InstructionsText("[grey](Press [blue]<space>[/] to toggle, [green]<enter>[/] to confirm)[/]");

            foreach (var flashcard in flashcards)
            {
                multiPrompt.AddChoice(flashcard.ToString());
            }

            var choices = AnsiConsole.Prompt(multiPrompt);
            return flashcards
                .Where(s => choices.Contains(s.ToString()))
                .Select(s => s.Id)
                .ToList();
        }
    }
}
