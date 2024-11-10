namespace KoiFish_Core.Models.Responses
{
    public class UserResponse
    {
                public Guid UserId { get; set; }

        public string FullName;
        public string Email;
        public string? Gender { get; set; }       
        public int? BirthYear { get; set; }
        public string? Element { get; set; }
                public string? Avatar { get; set; }
      public bool Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public string? PhoneNumber { get; set; }
        public string Role { get; set; }
    }
}