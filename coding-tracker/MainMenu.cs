using coding_tracker;
using Spectre.Console;

namespace Coding_Tracker;

internal class MainMenu
{
    internal void Menu()
    {
        Console.Clear();
        int count = 0; // prevent title from being displayed twice when data table is being displayed. 
        bool closeApp = false;
        
        while (closeApp == false)
        {
            if (count == 0) ConsoleUI.DisplayTitle();

            var mainMenu = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("What would you like to do?")
                .PageSize(20)
                .AddChoices(new[]
                {
                    "View All Data", "Insert Data", "Update Data", "Delete Data", "Filter Data", "Exit"
                }));
            
            switch (mainMenu)
            {
                case "Exit":
                    Console.WriteLine("\nGoodbye!\n");
                    closeApp = true;
                    Environment.Exit(0);
                    break;
                case "View All Data":
                    ConsoleUI.ShowCodingSession();
                    break;
                case "Insert Data":
                    CodingSessionTable.Insert();
                    break;
                case "Update Data":
                    CodingSessionTable.Update();
                    break;
                case "Delete Data":
                    CodingSessionTable.Delete();
                    break;
                case "Filter Data":
                    FilterMenu();
                    break;
                default:
                    Console.WriteLine("\nInvalid Command.\n");
                    break;
                    
            }
            
            count += 1;
        }
    }

    internal static void FilterMenu()
    {
        var filterMenu = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("How would you like to filter the data?")
            .PageSize(20)
            .AddChoices(new[]
            {
                "By Month", "By Year", "Return to Main Menu"
            }));

        var menu = new MainMenu();

        switch (filterMenu)
        {
            case "By Month":
                ConsoleUI.ShowMonthly();
                break;
            case "By Year":
                ConsoleUI.ShowYearly();
                break;
            case "Return to Main Menu":
                menu.Menu();
                break;
            default:
                Console.WriteLine("\nInvalid Command.\n");
                break;
        }
    }
}
