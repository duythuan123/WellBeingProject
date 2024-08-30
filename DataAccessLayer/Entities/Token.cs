using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Token
    {
        public int Id { get; set; }
        public string PasswordResetToken { get; set; }
        public DateTime? PasswordResetTokenExpiration { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
