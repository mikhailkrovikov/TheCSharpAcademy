using Flashcards.Controllers;
using Flashcards.Menus;

namespace Flashcards
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var cardStackController = new CardStackController();
            var flashcardController = new FlashcardController();

            var flashcardMenu = new FlashcardMenu(flashcardController);
            var stackMenu = new StackMenu(cardStackController, flashcardMenu);
            var mainMenu = new MainMenu(stackMenu);

            mainMenu.Run();
        }
    }
}
