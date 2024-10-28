namespace DataAccessLayer.Entities
{
    public partial class Psychiatrist
    {
        public Psychiatrist()
        {
            Appointments = new HashSet<Appointment>();
            TimeSlots = new HashSet<TimeSlot>();
        }

        public int Id { get; set; }
        public string Specialization { get; set; } = null!;
        public int? UserId { get; set; }
        public string? Bio { get; set; }
        public string? Experience { get; set; }
        public string? Location { get; set; }
        public decimal? ConsultationFee { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<TimeSlot> TimeSlots { get; set; }
    }
}
