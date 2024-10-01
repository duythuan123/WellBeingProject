using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.Response
{
    public class TimeSlotResponseModel
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string? DateOfWeek { get; set; }
        public string PsychiatristName { get; set; }
    }
}
