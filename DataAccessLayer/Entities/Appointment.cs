using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public partial class Appointment
    {
        public Appointment()
        {
            Payments = new HashSet<Payment>();
        }

        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; } = null!;
        public int UserId { get; set; }
        public int PsychiatristId { get; set; }
        public int? PaymentId { get; set; }

        public virtual Payment? Payment { get; set; }
        public virtual Psychiatrist Psychiatrist { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
