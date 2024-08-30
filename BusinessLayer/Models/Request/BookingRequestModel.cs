using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.Request
{
    public class BookingRequestModel
    {
        public int PsychiatristId { get; set; }
        public DateTime AppointmentDate  { get; set; }
    }
}
