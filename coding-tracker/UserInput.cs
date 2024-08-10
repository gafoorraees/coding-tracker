using System.Globalization;
namespace coding_tracker
{
    internal class UserInput
    {
        public static string GetSessionDescription(string prompt, bool isUpdate, string currentDescription = "")
        {
            Console.WriteLine(isUpdate ?
                $"{prompt} (current: {currentDescription}, press Enter to keep current):" :
                prompt);

            string input = Console.ReadLine();

            if (!isUpdate)
            {
                while (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Description cannot be empty. Please enter a valid description");
                    input = Console.ReadLine();
                }
            }

            return string.IsNullOrEmpty(input) ? currentDescription : input;
        }

        public static DateTime GetUserTime(string prompt, bool isUpdate, DateTime currentTime = default)
        {
            Console.WriteLine(isUpdate ?
                $"{prompt} (Current: {currentTime.ToString("HH:mm")}, press Enter to keep current):" :
                prompt);

            string input = Console.ReadLine();

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
                $"{prompt} (Current: {currentDate.ToString("MM/dd/yyyy")}, press Enter to keep current):" :
                prompt);

            string input = Console.ReadLine();

            DateTime date;
            var cultureInfo = new CultureInfo("en-US");

            while (!DateTime.TryParseExact(input, "MM/dd/yyyy", cultureInfo, DateTimeStyles.None, out date))
            {
                if (isUpdate && string.IsNullOrEmpty(input))
                {
                    return currentDate;
                }

                Console.WriteLine("Invalid input. Please enter the date and time in the format MM/dd/yyyy");
                input = Console.ReadLine();
            }

            return string.IsNullOrEmpty(input) ? currentDate : date;
        }
    }
}
