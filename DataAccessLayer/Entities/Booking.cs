using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; } //Pending, Confirmed, Cancelled
        public int UserId { get; set; }
        public User User { get; set; }
        public int PsychiatristId { get; set; }
        public Psychiatrist Psychiatrist { get; set; }
    }
}
