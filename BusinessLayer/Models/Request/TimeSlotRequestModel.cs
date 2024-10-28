namespace BusinessLayer.Models.Request
{
    public class TimeSlotRequestModel
    {
        public int TimeSlotId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime SlotDate { get; set; }
    }

    public class TimeSlotRequestModelForUpdate
    {
        public int TimeSlotId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime? SlotDate { get; set; }
    }

    public class TimeSlotModel
    {
        public int TimeSlotId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime? SlotDate { get; set; }
        public string PsychiatristName { get; set; }
    }
}
