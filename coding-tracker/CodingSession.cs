namespace coding_tracker
{
    public class CodingSession
    {   
        public int Id { get; set; }
        public string Session_description { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan CalculateDuration
        {
            get
            {
                if (EndTime < StartTime)
                {
                    return (EndTime.AddDays(1) - StartTime);
                }
                return EndTime - StartTime;
            }
        }
        public int Hours => CalculateDuration.Hours;
        public int Minutes => CalculateDuration.Minutes;
        public string DurationString => $"{Hours} hours, {Minutes} minutes";


        public void InitializeCodingSession(bool isUpdate = false)
        {

            Session_description = UserInput.GetSessionDescription("Please enter a description of your coding session\n", isUpdate, Session_description);
            Date = UserInput.GetUserDate("Please enter the date (yyyy-MM-dd)\n", isUpdate, Date);
            StartTime = UserInput.GetUserTime("Please enter the start time (HH:mm) - 24 hour clock.\n", isUpdate, StartTime);
            EndTime = UserInput.GetUserTime("Please enter the end time (HH:mm) - 24 horu clock.\n", isUpdate, EndTime);
        }
    }
}
