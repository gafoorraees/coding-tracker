using Coding_Tracker;
using System.ComponentModel.Design;
using System.Globalization;

namespace coding_tracker;

internal class UserInput
{
    public static string GetSessionDescription(string prompt, bool isUpdate, string currentDescription = "")
    {
        Console.WriteLine(isUpdate ?
            $"{prompt} (current: {currentDescription}, press Enter to keep current):" :
            prompt);

        string input = Console.ReadLine().Trim();

        if (input == "0")
        {
            MainMenu.ReturnToMenu();
        }

        if (string.IsNullOrEmpty(input) && !isUpdate)
        {
            Console.WriteLine("Invalid input. Please provide a valid session description.");
            return GetSessionDescription(prompt, isUpdate, currentDescription);
        }
        else if (string.IsNullOrEmpty(input) && isUpdate)
        {
            return currentDescription;
        }


        Validation.GetStringInput(input);

        return input;
    }

    public static DateTime GetUserTime(string prompt, bool isUpdate, DateTime currentTime = default)
    {
        Console.WriteLine(isUpdate ?
            $"{prompt} (Current: {currentTime.ToString("HH:mm")}, press Enter to keep current):" :
            prompt);

        string input = Console.ReadLine();

        if (input == "0")
        {
            MainMenu.ReturnToMenu();
        }

        DateTime time;
        var cultureInfo = new CultureInfo("en-US");

        while (!DateTime.TryParseExact(input, "HH:mm", cultureInfo, DateTimeStyles.None, out time))
        {
            if (isUpdate && string.IsNullOrEmpty(input))
            {
                return currentTime;
            }

            Console.WriteLine("Invalid input. Please enter the time in the format HH:mm");
            input = Console.ReadLine();
        }

        return string.IsNullOrEmpty (input) ? currentTime : time; 
    }

    public static DateTime GetUserDate(string prompt, bool isUpdate, DateTime currentDate = default)
    {
        Console.WriteLine(isUpdate ? 
            $"{prompt} (Current: {currentDate.ToString("yyyy-MM-dd")}, press Enter to keep current):" :
            prompt);

        string input = Console.ReadLine();

        if (input == "0")
        {
            MainMenu.ReturnToMenu();
        }

        DateTime localDate = DateTime.Now.Date;
        DateTime date;
        var cultureInfo = new CultureInfo("en-US");

        if (isUpdate && string.IsNullOrEmpty(input))
        {
            return currentDate;
        }

        while (true)
        {
            if (DateTime.TryParseExact(input, "yyyy-MM-dd", cultureInfo, DateTimeStyles.None, out date))
            {
                if (date > localDate)
                {
                    Console.WriteLine("The entered date cannot be in the future. Please try again.");
                }
                else
                {
                    return date;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter the date in the format yyy-MM-dd.");
            }

            input = Console.ReadLine();
        }
    }

    public static DateTime GetUserFilterMonth(string prompt)
    {
        Console.WriteLine(prompt);
        string input = Console.ReadLine();
        DateTime date;
        var cultureInfo = new CultureInfo("en-US");

        while (!DateTime.TryParseExact(input, "yyyy-MM", cultureInfo, DateTimeStyles.None, out date))
        {
            Console.WriteLine("Invalid input. Please enter the date and time in the format yyyy-MM");
            input = Console.ReadLine();
        }

        return new DateTime(date.Year, date.Month, 1);
    }
}
