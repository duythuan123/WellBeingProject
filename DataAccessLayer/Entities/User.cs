namespace DataAccessLayer.Entities
{
    public partial class User
    {
        public User()
        {
            Appointments = new HashSet<Appointment>();
            Psychiatrists = new HashSet<Psychiatrist>();
            Tokens = new HashSet<Token>();
        }

        public int Id { get; set; }
        public string Fullname { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string Phonenumber { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string? Role { get; set; }
        public string? UserImage { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Psychiatrist> Psychiatrists { get; set; }
        public virtual ICollection<Token> Tokens { get; set; }
    }
}
