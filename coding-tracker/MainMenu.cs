using coding_tracker;
using Spectre.Console;

namespace Coding_Tracker
{
    internal class MainMenu
    {
        internal void Menu()
        {
            Console.Clear();
            bool closeApp = false;
            while (closeApp == false)
            {
                var mainMenu = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("What would you like to do?")
                    .PageSize(10)
                    .AddChoices(new[]
                    {
                        "View All Data", "Insert Data", "Delete Data", "Update Data"
                    }));

                switch (mainMenu)
                {
                    case "":
                        Console.WriteLine("\nGoodbye!\n");
                        closeApp = true;
                        Environment.Exit(0);
                        break;
                    case "View All Data":
                        CodingSessionTable.ViewData();
                        break;
                    case "Insert Data":
                        CodingSessionTable.Insert();
                        break;
                    case "Delete Data":
                        CodingSessionTable.Delete();
                        break;
                    case "Update Data":
                        CodingSessionTable.Update();
                        break;
                    default:
                        Console.WriteLine("\nInvalid Command. Please type a number from 0 to 4.\n");
                        break;
                        
                }

            }
        }
    }
}
