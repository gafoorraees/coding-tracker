using System.Configuration;
using Dapper;
using Microsoft.Data.Sqlite;

namespace coding_tracker;

public class CodingSessionTable
{
    private static string connectionString = ConfigurationManager.AppSettings.Get("connectionString");
    
    public void CreateTable()
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            using (var tableCmd = connection.CreateCommand())
            {
                tableCmd.CommandText =
                    @"CREATE TABLE IF NOT EXISTS coding_sessions (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Session_description TEXT,
                            Date TEXT,
                            StartTime TEXT,
                            EndTime Text,
                            Duration TEXT
                        )";

                tableCmd.ExecuteNonQuery();
            }
        }
    }

    public static IEnumerable<CodingSession> GetData()
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            string sql = "SELECT * FROM coding_sessions";
            var codingSessions = connection.Query<CodingSession>(sql).ToList();
            return codingSessions;
        }
    }

    public static IEnumerable<CodingSession> GetMonthlyData()
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            Console.WriteLine("Please enter the month for which you would like to view data in format MM ");
            var month = Console.ReadLine();
            Console.WriteLine("Please enter the year for the month that you would like to view data in format yyyy");
            var year = Console.ReadLine();

            if (int.TryParse(month, out int monthInt) && int.TryParse(year, out int yearInt))
            {
                DateTime startDate = new DateTime(yearInt, monthInt, 1);
                DateTime endDate = startDate.AddMonths(1).AddDays(-1);

                string sql = @"
                        SELECT *
                        FROM coding_sessions
                        WHERE Date BETWEEN @StartDate AND @EndDate
                        ORDER BY Date ASC";

                var monthlySessions = connection.Query<CodingSession>(sql, new
                {
                    StartDate = startDate.ToString("yyyy-MM-dd"),
                    EndDate = endDate.ToString("yyyy-MM-dd")
                }).ToList();

                return monthlySessions;
            }
            else
            {
                Console.WriteLine("Invalid month or year entered.");
                return new List<CodingSession>();
            }
        }
    }

    public static IEnumerable<CodingSession> GetYearlyData()
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            Console.WriteLine("Please enter the year for which you would like to view data in the format yyyy");
            var year = Console.ReadLine();
            

            if (int.TryParse(year, out int yearInt))
            {
                DateTime startDate = new DateTime(yearInt, 2, 2);
                DateTime endDate = new DateTime(yearInt, 12, 31);

                string sql = @"
                        SELECT *
                        FROM coding_sessions
                        WHERE Date BETWEEN @StartDate AND @EndDate
                        ORDER BY Date ASC";

                var yearlySessions = connection.Query<CodingSession>(sql, new
                {
                    StartDate = startDate.ToString("yyyy-MM-dd"),
                    EndDate = endDate.ToString("yyyy-MM-dd")
                }).ToList();

                return yearlySessions;
            }
            else
            {
                Console.WriteLine("Invalid year entered");
                return new List<CodingSession>();
            }
        }
    }

    public static void Insert()
    {   
        Console.Clear();
        ConsoleUI.DisplayTitle();
        CodingSession newSession = new CodingSession();
        newSession.InitializeCodingSession(false);

        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            using (var insertCmd = connection.CreateCommand())
            {
                insertCmd.CommandText = @"
                        INSERT INTO coding_sessions (Session_description, Date, StartTime, EndTime)
                        VALUES (@Description, @Date, @StartTime, @EndTime)";

                insertCmd.Parameters.AddWithValue("Description", newSession.Session_description);
                insertCmd.Parameters.AddWithValue("Date", newSession.Date.ToString("yyyy-MM-dd"));
                insertCmd.Parameters.AddWithValue("StartTime", newSession.StartTime.ToString("HH:mm"));
                insertCmd.Parameters.AddWithValue("EndTime", newSession.EndTime.ToString("HH:mm"));

                insertCmd.ExecuteNonQuery();
            }
        }
    }

    public static void Update()
    {
        while (true)
        {
            Console.Clear();
            ConsoleUI.DisplayTitle();
            ConsoleUI.ShowCodingSession();
            var sessionId = Validation.GetNumberInput("\n\n Please type the Id of the session that you would like to update, or press 0 to return to the main menu.\n\n");

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var session = connection.QuerySingleOrDefault<CodingSession>(
                    "SELECT * FROM coding_sessions WHERE Id = @SessionId", new
                    {
                        SessionId = sessionId
                    });

                if (session == null)
                {
                    Console.WriteLine("Session not found. Please try again.");
                    continue;
                }

                string description = UserInput.GetSessionDescription("Please enter updated session description", true, session.Session_description);
              
                DateTime date = UserInput.GetUserDate("Please enter updated date", true, session.Date);               

                DateTime startTime = UserInput.GetUserTime("Please enter updated start time", true, session.StartTime);
               
                DateTime endTime = UserInput.GetUserTime("Please enter updated end time", true, session.EndTime);
           
                string updateSql = @"
                        UPDATE coding_sessions
                        SET Session_description = @Description, Date = @Date, StartTime = @StartTime, EndTime = @EndTime
                        WHERE Id = @SessionId";

                connection.Execute(updateSql, new
                {
                    Description = description,
                    Date = date,
                    StartTime = startTime,
                    EndTime = endTime,
                    SessionId = sessionId
                });

                Console.WriteLine("Session updated successfully.");
                break;
            }
        }
    }

    public static void Delete()
    {
        Console.Clear();
        ConsoleUI.DisplayTitle();

        while (true)
        {
            ConsoleUI.ShowCodingSession();

            var sessionId = Validation.GetNumberInput("\n\nPlease type the Id of the session that you woulld like to delete, or press 0 to return to the main menu\n\n");
   
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var session = connection.QuerySingleOrDefault<CodingSession>(
                    "SELECT * FROM coding_sessions WHERE Id = @SessionId", new
                    {
                        SessionId = sessionId
                    });

                if (session == null)
                {
                    Console.WriteLine("Session not found. Please try again.");
                    continue;

                }

                string deleteSql = @"
                        DELETE FROM coding_sessions
                        WHERE Id = @SessionId";

                connection.Execute(deleteSql, new
                {
                    SessionId = sessionId
                });

                Console.WriteLine("Session deleted successfully.");
                break;
            }
        }
    }
}
