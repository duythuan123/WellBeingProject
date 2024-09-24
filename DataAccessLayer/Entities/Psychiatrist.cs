using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public partial class Psychiatrist
    {
        public Psychiatrist()
        {
            Bookings = new HashSet<Booking>();
        }

        public int Id { get; set; }
        public string Specialization { get; set; } = null!;
        public int UserId { get; set; }
        public string? Bio { get; set; }
        public string? Experience { get; set; }
        public string? Location { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
