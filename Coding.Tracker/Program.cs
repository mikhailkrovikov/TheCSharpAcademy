using System.Diagnostics;
using System.Globalization;

namespace Coding.Tracker
{
    public static class Program
    {
        private static CodeSessionService service;
        static void Main(string[] args)
        {

            service = new CodeSessionService();
            service.CreateDatabase();

            while (true)
            {
                try
                {
                    ResetConsole();
                    var choice = GetUserAction(Console.ReadLine());

                    if (choice == UserAction.CreateSession)
                    {
                        CreateSession();
                    }

                    if (choice == UserAction.StartSession)
                    {
                        StartSession();
                    }

                    if (choice == UserAction.ReadSessions)
                    {
                        PrintAllData();
                        Console.WriteLine("\nPress any key to return to Menu");
                        Console.ReadKey();
                    }
                    if (choice == UserAction.UpdateSession)
                    {
                        UpdateSession();
                    }
                    if (choice == UserAction.DeleteSession)
                    {
                        DeleteSession();
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                }
            }
        }

        private static void ResetConsole()
        {
            Console.Clear();
            SpectreConsoleUI.PrintMessage("Choose action S C R U D ");
        }



        private static DateTime ValidateDateTime(string input)
        {
            var valid = DateTime.TryParseExact(
                input,
                "dd-MM-yy HH:mm:ss",
                new CultureInfo("en-US"),
                DateTimeStyles.None, out DateTime result);

            if (valid)
            {
                return result;
            }
            else throw new FormatException($"Wrong format of value: {input}! Should be dd-MM-yy HH:mm:ss");

        }

        private static int ValidateNumeric(string input)
        {
            var valid = int.TryParse(input, out int result);
            if (valid)
            {
                return result;
            }
            else throw new FormatException($"Wrong format of value: {input}! Should be numeric");
        }

        private static UserAction GetUserAction(string input)
        {
            switch (input)
            {
                case "S":
                    return UserAction.StartSession;
                case "C":
                    return UserAction.CreateSession;
                case "R":
                    return UserAction.ReadSessions;
                case "U":
                    return UserAction.UpdateSession;
                case "D":
                    return UserAction.DeleteSession;
                default: throw new ArgumentException("This type of action is not supported");
            }
        }


        private static void StartSession()
        {
            SpectreConsoleUI.PrintMessage("To start session press any key...");
            Console.ReadKey();

            var startTime = DateTime.Now;
            Console.Clear();

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var lastSecond = 0d;
            while (!Console.KeyAvailable)
            {
                var currentSecond = stopwatch.Elapsed.TotalSeconds;
                if (currentSecond != lastSecond)
                {
                    lastSecond = currentSecond;
                    var elapsed = stopwatch.Elapsed;
                    SpectreConsoleUI.PrintMessage($"\rElapsed: {(int)elapsed.TotalHours:D2}:{elapsed.Minutes:D2}:{elapsed.Seconds:D2}. Press any key to stop");
                }
            }
            Console.ReadKey(true);
            SpectreConsoleUI.PrintMessage(string.Empty);
            stopwatch.Stop();

            var endTime = DateTime.Now;
            var codeSession = new CodeSession
            {
                StartTime = startTime.ToString("dd-MM-yy HH:mm:ss"),
                EndTime = endTime.ToString("dd-MM-yy HH:mm:ss"),
                Duration = TimeOnly.FromTimeSpan(endTime - startTime).ToString("HH:mm:ss")
            };

            if (!service.Create(codeSession))
            {
                throw new ArgumentException("Error occures when adding to database");
            }
            SpectreConsoleUI.PrintMessage("Session was succesfully created. Press any key to return to Menu", "green");
            Console.ReadKey();
        }

        private static void CreateSession()
        {
            SpectreConsoleUI.PrintMessage("Enter starttime of session in dd-MM-yy HH:mm:ss format");
            var startTime = ValidateDateTime(Console.ReadLine());

            SpectreConsoleUI.PrintMessage("Enter endTime of session in dd-MM-yy HH:mm:ss format");
            var endTime = ValidateDateTime(Console.ReadLine());

            if (endTime <= startTime)
            {
                throw new ArgumentException($"StartTime {startTime} cannot be greater then EndTime {endTime}");
            }

            var session = new CodeSession
            {
                StartTime = startTime.ToString("dd-MM-yy HH:mm:ss"),
                EndTime = endTime.ToString("dd-MM-yy HH:mm:ss"),
                Duration = TimeOnly.FromTimeSpan(endTime - startTime).ToString("HH:mm:ss")
            };

            if (!service.Create(session))
            {
                throw new ArgumentException("Error occures when adding to database");
            }
            SpectreConsoleUI.PrintMessage("Session was succesfully created. Press any key to return to Menu", "green");
            Console.ReadKey();
        }

        private static void PrintAllData()
        {
            var list = service.ReadAllData();
            SpectreConsoleUI.PrintTable(list);
        }

        private static void UpdateSession()
        {
            PrintAllData();
            SpectreConsoleUI.PrintMessage("Enter [blue]id[/] of session for Update");
            var id = ValidateNumeric(Console.ReadLine());
            var session = service.ReadAllData().FirstOrDefault(s => s.Id == id);
            if (session == null)
            {
                SpectreConsoleUI.PrintMessage($"Session with {id} not found", "yellow");
                Console.ReadKey();
            }
            else
            {
                SpectreConsoleUI.PrintMessage("Enter starttime of session in dd-MM-yy HH:mm:ss format");
                var startTime = ValidateDateTime(Console.ReadLine());

                SpectreConsoleUI.PrintMessage("Enter endTime of session in dd-MM-yy HH:mm:ss format");
                var endTime = ValidateDateTime(Console.ReadLine());

                if (endTime <= startTime)
                {
                    throw new ArgumentException($"StartTime {startTime} cannot be greater then EndTime {endTime}");
                }

                session.StartTime = startTime.ToString("dd-MM-yy HH:mm:ss");
                session.EndTime = endTime.ToString("dd-MM-yy HH:mm:ss");
                session.Duration = TimeOnly.FromTimeSpan(endTime - startTime).ToString("HH:mm:ss");

                if (!service.Update(session))
                {
                    throw new ArgumentException("Error occures when updating of database");
                }

                SpectreConsoleUI.PrintMessage("Session was succesfully updated. Press any key to return to Menu", "green");
                Console.ReadKey();
            }
        }

        private static void DeleteSession()
        {
            PrintAllData();
            SpectreConsoleUI.PrintMessage("Enter [blue]id[/] of session for delete");
            var id = ValidateNumeric(Console.ReadLine());
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
