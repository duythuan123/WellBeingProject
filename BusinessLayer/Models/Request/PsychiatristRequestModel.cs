using DataAccessLayer.Entities;

namespace BusinessLayer.Models.Request
{
    public class PsychiatristRequestModel
    {
        public string Specialization { get; set; } = null!;
        public int? UserId { get; set; }
        public string? Bio { get; set; }
        public string? Experience { get; set; }
        public string? Location { get; set; }

        public virtual User? User { get; set; }
    }

    public class PsychiatristRequestModelForUpdate
    {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phonenumber { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string? UserImage { get; set; }
        public decimal? ConsultationFee { get; set; }
        public string Specialization { get; set; } = null!;
        public string? Bio { get; set; }
        public string? Experience { get; set; }
        public string? Location { get; set; }

    }
}
