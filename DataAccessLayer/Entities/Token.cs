using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public partial class Token
    {
        public int Id { get; set; }
        public string PasswordResetToken { get; set; } = null!;
        public DateTime? PasswordResetTokenExpiration { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
