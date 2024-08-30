using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phonenumber { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public ICollection<Token> Tokens { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
