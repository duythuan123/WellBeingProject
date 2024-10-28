namespace BusinessLayer.Models.Response
{
    public class UserResponseModel
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phonenumber { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string? Role { get; set; }
        public string? UserImage { get; set; }

    }
}
