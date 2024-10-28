namespace DataAccessLayer.Entities
{
    public partial class Payment
    {
        public Payment()
        {
            Appointments = new HashSet<Appointment>();
        }

        public int Id { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? PaymentStatus { get; set; }
        public int? AppointmentId { get; set; }

        public virtual Appointment? Appointment { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
