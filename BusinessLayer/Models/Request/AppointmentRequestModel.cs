using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.Request
{
    public class AppointmentRequestModel
    {
        public DateTime BookingDate { get; set; }
        public DateTime AppointmentDate { get; set; }
        [DefaultValue("6")]
        public int UserId { get; set; }
        [DefaultValue("2")]
        public int PsychiatristId { get; set; }
        [DefaultValue("2")]
        public int? TimeSlotId { get; set; }
        public string? Reason { get; set; }
    }

    public class AppointmentRequestModelForUpdate
    {
        public DateTime BookingDate { get; set; }
        public DateTime AppointmentDate { get; set; }
        [DefaultValue("CREATED")]
        public string Status { get; set; } = null!;
        public int UserId { get; set; }
        public int PsychiatristId { get; set; }
        public int? TimeSlotId { get; set; }
        public string? Reason { get; set; }
    }

}
