namespace BusinessLayer.Models.Response
{
    public class AppointmentResponseModel
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; } = null!;
        public int UserId { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public int PsychiatristId { get; set; }
        public int? PaymentId { get; set; }
        public int? TimeSlotId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string? Reason { get; set; }
        public decimal? ConsultationFee { get; set; }
    }
}
