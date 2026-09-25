using Spectre.Console;

namespace Coding.Tracker
{
    public static class SpectreConsoleUI
    {
        public static void PrintTable(List<CodeSession> sessions)
        {
            var table = new Table()
                .AddColumn("Id")
                .AddColumn("Start time")
                .AddColumn("End time")
                .AddColumn("Duration");

            foreach(var session in sessions)
            {
                table.AddRow(
                    $"{session.Id}",
                    $"{session.StartTime}",
                    $"{session.EndTime}", 
                    $"{session.Duration}");
            }

            AnsiConsole.Write(table);
        }

        public static void PrintMessage(string message, string color)
        {
            AnsiConsole.MarkupLine($"[{color}] {message}[/]");
        }
        public static void PrintMessage(string message)
        {
            PrintMessage(message, "white");
        }
    }
}
