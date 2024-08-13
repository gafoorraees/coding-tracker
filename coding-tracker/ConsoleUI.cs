using Spectre.Console;

namespace coding_tracker
{
    internal class ConsoleUI
    {
        public static void DisplayTable(IEnumerable<CodingSession> sessions)
        {
            ConsoleUI.DisplayTitle();
            var table = new Table();

            table.AddColumn("Id");
            table.AddColumn("Description");
            table.AddColumn("Date");
            table.AddColumn("Start Time");
            table.AddColumn("End Time");
            table.AddColumn("Duration");

            foreach (var session in sessions)
            {
                table.AddRow(
                    session.Id.ToString(),
                    session.Session_description,
                    session.Date.ToShortDateString(),
                    session.StartTime.ToShortTimeString(),
                    session.EndTime.ToShortTimeString(),
                    session.DurationString);
            }

            AnsiConsole.Write(table);
        }


        public static void ShowCodingSession()
        {
            Console.Clear();

            var sessions = CodingSessionTable.GetData();
            DisplayTable(sessions);
        }

        public static void ShowMonthly()
        {


            Console.Clear();

            var sessions = CodingSessionTable.GetMonthlyData();
            DisplayTable(sessions);
        }

        public static void ShowYearly()
        {
            Console.Clear();

            var sessions = CodingSessionTable.GetYearlyData();
            DisplayTable(sessions);
        }
        public static void DisplayTitle()
        {
            var panel = new Panel("Coding Tracker");
            panel.Expand = true;
            panel.Border = BoxBorder.Double;
            AnsiConsole.Write(panel);
        }

    }
}
