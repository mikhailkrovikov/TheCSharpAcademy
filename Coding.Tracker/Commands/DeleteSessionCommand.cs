using Spectre.Console;

namespace Coding.Tracker.Commands
{
    public class DeleteSessionCommand : Command
    {
        public DeleteSessionCommand(CodeSessionService service) : base(service)
        {
        }

        public override void Execute()
        {
            var reader = new ViewDataCommand(service);
            reader.ExecuteWithoutExit();
            var id = ValidateNumeric(AnsiConsole.Ask<string>("Enter numeric [blue]id[/] of session for delete:"));
            var session = service.ReadAllData().FirstOrDefault(s => s.Id == id);
            if (session == null)
            {
                SpectreConsoleUI.PrintMessage($"Session with {id} not found", "yellow");
                Console.ReadKey();
            }
            else
            {
                if (!service.Delete(id))
                {
                    throw new ArgumentException("Error occures when delete form database");
                }
                SpectreConsoleUI.PrintMessage("Session was succesfully deleted. Press any key to return to Menu", "green");
                Console.ReadKey();
            }
        }
    }
}
