using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public partial class TimeSlot
    {
        public int TimeSlotId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int? PsychiatristId { get; set; }
        public string? DateOfWeek { get; set; }

        public virtual Psychiatrist? Psychiatrist { get; set; }
    }
}
