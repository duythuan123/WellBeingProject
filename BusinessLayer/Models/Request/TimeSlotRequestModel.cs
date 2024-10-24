using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.Request
{
    public class TimeSlotRequestModel
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string? DateOfWeek { get; set; }
    }

    public class TimeSlotRequestModelForUpdate
    {
        public int TimeSlotId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string? DateOfWeek { get; set; }
    }

    public class TimeSlotModel
    {
        public int TimeSlotId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string? DateOfWeek { get; set; }
        public string PsychiatristName { get; set; }
    }
}
