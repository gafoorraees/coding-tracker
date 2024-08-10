using System.Configuration;
using Dapper;
using Microsoft.Data.Sqlite;

namespace coding_tracker
{
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

        public static void ViewData()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM coding_sessions";
                var codingSessions = connection.Query<CodingSession>(sql).ToList();

                Console.Clear();

                foreach (var session in codingSessions)
                {
                    Console.WriteLine("\n");
                    Console.WriteLine($"Id: {session.Id}");
                    Console.WriteLine($"Description: {session.Session_description}");
                    Console.WriteLine($"Date: {session.Date.ToShortDateString()}");
                    Console.WriteLine($"Start Time: {session.StartTime.ToShortTimeString()}");
                    Console.WriteLine($"EndTime: {session.EndTime.ToShortTimeString()}");
                    Console.WriteLine($"Duration: {session.DurationString}");
                    Console.WriteLine("\n");
                }
            }
        }

        public static void Insert()
        {
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
                    insertCmd.Parameters.AddWithValue("Date", newSession.Date.ToString("MM/dd/yyyy"));
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
                ViewData();

                var sessionId = Validation.GetNumberInput("\n\n Please type the Id of the session that you would like to update.\n\n");

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
            while (true)
            {
                ViewData();

                var sessionId = Validation.GetNumberInput("\n\nPlease type the Id of the session that you woulld like to delete.\n\n");

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
}
