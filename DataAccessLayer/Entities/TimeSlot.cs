using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public partial class TimeSlot
    {
        public TimeSlot()
        {
            Appointments = new HashSet<Appointment>();
        }

        public int TimeSlotId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int? PsychiatristId { get; set; }
        public string? DateOfWeek { get; set; }
        public string? Status { get; set; }

        public virtual Psychiatrist? Psychiatrist { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
