using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.Response
{
    public class PsychiatristResponseModel
    {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phonenumber { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string? UserImage { get; set; }
        public string Specialization { get; set; } = null!;
        public string? Bio { get; set; }
        public string? Experience { get; set; }
        public string? Location { get; set; }
    }
}
