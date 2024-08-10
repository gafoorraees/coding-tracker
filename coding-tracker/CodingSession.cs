namespace coding_tracker
{
    public class CodingSession
    {   
        public int Id { get; set; }
        public string Session_description { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan CalculateDuration => (EndTime - StartTime).Duration();
        public int Hours => CalculateDuration.Hours;
        public int Minutes => CalculateDuration.Minutes;
        public string DurationString => $"{Hours} hours, {Minutes} minutes";


        public void InitializeCodingSession(bool isUpdate = false)
        {
            Session_description = UserInput.GetSessionDescription("Please enter a description of your coding session.", isUpdate, Session_description);
            Date = UserInput.GetUserDate("Please enter the date (MM/dd/yyyy)", isUpdate, Date);
            StartTime = UserInput.GetUserTime("Please enter the start time (HH:mm)", isUpdate, StartTime);
            EndTime = UserInput.GetUserTime("Please enter the end time (HH:mm)", isUpdate, EndTime);
        }
    }
}
