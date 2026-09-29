namespace Flashcards.Menus
{
    public class MainMenu
    {
        private readonly StackMenu stackMenu;

        public MainMenu(StackMenu stackMenu)
        {
            this.stackMenu = stackMenu;
        }

        public void Run()
        {
            while (true)
            {
                var result = UI.GetActions(new List<string>
                {
                    "Study",
                    "Manage stacks",
                    "Exit"
                });

                if (result == "Manage stacks")
                    stackMenu.Run();

                if (result == "Exit")
                    return;
            }
        }
    }
}
