namespace CvManager.Models
{
    public class ExtraCourse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Course { get; set; }
        public string? Institution { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public User? User { get; set; }
    }

}