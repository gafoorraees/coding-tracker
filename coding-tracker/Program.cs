using coding_tracker;

namespace Coding_Tracker
{
    class Program
    {
        static void Main(string[] args)
        {
            // string connectionString = ConfigurationManager.AppSettings.Get("connectionString");
            var codingSession = new CodingSessionTable();
            codingSession.CreateTable();
            
            var menu = new MainMenu();
            menu.Menu();

        }

    }
}