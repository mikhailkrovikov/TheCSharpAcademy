using Flashcards.Controllers;
using Flashcards.DTOs;
using Flashcards.Entities;
using Flashcards.Repositories;
using Microsoft.Extensions.Configuration;
using Spectre.Console;

namespace Flashcards
{
    internal class Program
    {
        public static FlashcardController flashcardController;

        public static CardStackController cardStackController;

        static void Main(string[] args)
        {
            flashcardController = new();
            cardStackController = new();

            while (true)
            {
                var result = UI.GetActions(new List<string>
                {
                    "Study",
                    "Manage stacks",
                    "Exit"
                });

                // Меню работы с стеком
                if (result == "Manage stacks")
                {
                    Console.Clear();
                    var data = cardStackController.ReadCardStacks().ToList();
                    UI.PrintStackTable(data);

                    // Выбор дейсввтия со стеком
                    var stackAction = UI.GetActions(new List<string>
                    {
                        "Add stack",
                        "Remove stack",
                        "Manage flashcards"
                    });

                    // Добавить стек
                    if (stackAction == "Add stack")
                    {
                        var name = Console.ReadLine();
                        var card = new CardStackDTO
                        {
                            Name = name,
                        };
                        cardStackController.CreateCardStack(card);
                    }

                    // Удалить стек
                    if (stackAction == "Remove stack")
                    {
                        var stacks = UI.PrintStackChoices(data);
                        foreach (var item in stacks)
                        {
                            cardStackController.DeleteCardStack(item);
                        }
                    }



                    // Работа с карточками
                    if (stackAction == "Manage flashcards")
                    {
                        var list = data.Select(c => c.Name).ToList();
                        var stackName = UI.GetActions(list);



                        var stackOwner = data.FirstOrDefault(c => c.Name == stackName);


                        // Меню выбора децсвтия с карточкой
                        var flashactions = UI.GetActions(new List<string>
                        {
                            "View flashcards",
                            "Add flashcard",
                            "Remove flashcards"
                        });

                        // Показать карточки
                        if (flashactions == "View flashcards")
                        {
                            var flashCards = flashcardController.GetFlashcards(stackOwner.Id).ToList();
                            UI.PrintFlashcardTable(flashCards);
                        }

                        // Добавить карточку
                        if (flashactions == "Add flashcard")
                        {
                            var front = AnsiConsole.Ask<string>("Enter front [blue]text[/] of flashcard:");
                            var back = AnsiConsole.Ask<string>("Enter back [blue]text[/] of flashcard:");
                            var flashcard = new CreateFlashcardDTO
                            {
                                CardStackId = stackOwner.Id,
                                Front = front,
                                Back = back,
                            };
                            flashcardController.CreateFlashCard(flashcard);
                        }

                        //if (flashactions == "Remove flashcards")
                        //{
                            
                        //    UI.PrintFlashcardChoices()
                        //    flashcardController.();
                        //}
                    }
                }
            }
        }
    }
}
