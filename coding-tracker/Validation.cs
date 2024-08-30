using Coding_Tracker;

namespace coding_tracker;

internal class Validation
{
    public static int GetNumberInput(string message)
    {
        Console.WriteLine(message);

        string numberInput = Console.ReadLine();
        
        var menu = new MainMenu();

        if (numberInput == "0") menu.Menu();

        while (!Int32.TryParse(numberInput, out _) || Convert.ToInt32(numberInput) < 0)
        {
            Console.WriteLine("\n\nInvalid number. Try again.\n\n");
            numberInput = Console.ReadLine();
        }

        int finalInput = Convert.ToInt32(numberInput);

        return finalInput;
    }

    public static string GetStringInput(string input)
    {
        int testNumeric;
  
        while (string.IsNullOrEmpty(input) || int.TryParse(input, out testNumeric))
        {
            Console.WriteLine("Invalid description input. Please enter a valid description.");
            input = Console.ReadLine();
        }

        return input;
    }
}
