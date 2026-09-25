using Coding.Tracker.Commands;
using Microsoft.Extensions.Configuration;
using Spectre.Console;

namespace Coding.Tracker
{
    public static class Program
    {
        private static CodeSessionService service;
        static void Main(string[] args)
        {
            if (!Configurate()) return;
            service.CreateDatabase();
            while (true)
            {
                try
                {
                    SpectreConsoleUI.Clear();
                    var choice = GetUserAction();
                    Execute(choice);
                }
                catch (Exception ex)
                {
                    SpectreConsoleUI.PrintMessage($"{ex.Message}", "red");
                    Console.ReadKey();
                }
            }
        }

        private static UserAction GetUserAction()
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("What [blue]action[/] would you like?")
                .AddChoices(
                    "Start new session",
                    "Add new session",
                    "View sessions",
                    "Update session",
                    "Delete session")
                );
            return MapString(choice);
        }

        private static UserAction MapString(string input)
        {
            switch (input)
            {
                case "Start new session":
                    return UserAction.StartSession;
                case "Add new session":
                    return UserAction.CreateSession;
                case "View sessions":
                    return UserAction.ReadSessions;
                case "Update session":
                    return UserAction.UpdateSession;
                case "Delete session":
                    return UserAction.DeleteSession;
                default: throw new ArgumentException("This type of action is not supported");
            }
        }
        
        private static void Execute(UserAction action)
        {
            Command command = default;
            if (action == UserAction.CreateSession)
                command = new CreateSessionCommand(service);

            else if (action == UserAction.StartSession)
                command = new StartSessionCommand(service);

            else if (action == UserAction.ReadSessions)
                command = new ViewDataCommand(service);

            else if (action == UserAction.UpdateSession)
                command = new UpdateSessionCommand(service);

            else if (action == UserAction.DeleteSession)
                command = new DeleteSessionCommand(service);

            else throw new ArgumentException("Invalid command");
            command.Execute();
        }

        private static bool Configurate()
        {
            try
            {
                var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                service = new CodeSessionService(connectionString);
                return true;
            }
            catch
            {
                SpectreConsoleUI.PrintMessage("Unable to load configuration");
                return false;
            }
        }
    }
}
