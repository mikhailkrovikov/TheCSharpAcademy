using Flashcards.Controllers;
using Flashcards.DTOs;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flashcards.Menus
{
    public class StudyMenu
    {
        private readonly CardStackController cardStackController;
        private readonly FlashcardController flashcardController;

        public StudyMenu(CardStackController cardStackController, FlashcardController flashcardController)
        {
            this.flashcardController = flashcardController;
            this.cardStackController = cardStackController;
        }

        public void Run()
        {
            var data = cardStackController.ReadCardStacks().ToList();
            while (true)
            {
                Console.Clear();

                var action = UI.GetActions(new List<string>
                {
                    "Choose stack",
                    "Exit"
                });

                if (action == "Exit")
                    return;
                if (action == "Choose stack")
                    ManageStacks(data);
            }

        }
        private void ManageStacks(List<CardStackDTO> data)
        {
            if (data.Count == 0)
                return;

            var list = data.Select(c => c.Name).ToList();
            var stackName = UI.GetActions(list);
            var stackOwner = data.First(c => c.Name == stackName);
            var studySession = new StudySession(flashcardController);
            studySession.StartGame(stackOwner);
        }
    }


    public class StudySession
    {
        private readonly FlashcardController flashcardController;
        private int score;

        public StudySession(FlashcardController flashcardController)
        {
            this.flashcardController = flashcardController;
        }

        public void StartGame(CardStackDTO cardStack)
        {
            score = 0;
            var flashcards = flashcardController.GetFlashcards(cardStack.Id);
            UI.PrintMessage("To stop studying, enter E instead of an answer.");
            foreach (var flashcard in flashcards)
            {
                if (!StudyCard(flashcard))
                {
                    UI.PrintMessage($"Study stopped: your score is {score}");
                    return;
                }
            }
            UI.PrintMessage($"Flashcards ended: your score is {score}");
            UI.PrintMessage("Press Enter to return to the study menu.");
            Console.ReadLine();
            Console.Clear();
        }


        private bool StudyCard(GetFlashcardDTO flashcard)
        {
            var answer = AnsiConsole.Ask<string>($"{flashcard.Front}: print a translation: ");
            if (answer.Trim().Equals("E", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (answer.Equals(flashcard.Back))
            {
                score++;
                UI.PrintMessage($"Correct, score is {score}");
            }
            else
            {
                UI.PrintMessage($"Incorrect, answer is {flashcard.Back}, score is {score}");
            }
            return true;
        }

    }
}
