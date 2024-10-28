namespace BusinessLayer.Models.Response
{
    public class TimeSlotResponseModel
    {
        public int TimeSlotId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime? SlotDate { get; set; }
        public string PsychiatristName { get; set; }
    }

}
