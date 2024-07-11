namespace CvManager.DTOs
{
    public class UserDTO
    {
        public string? Name { get; set; }
        public string? Age { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public byte[]? Image { get; set; }
        public string? Status { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}