using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Psychiatrist
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public string Specialization { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
