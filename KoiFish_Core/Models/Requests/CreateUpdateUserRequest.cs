namespace KoiFish_Core.Models.Requests
{
    public class CreateUserRequest
    {
        public string FullName {get; set;}
        public string Email {get; set;}
        public string? Gender { get; set; }       
        public int? BirthYear { get; set; }

        public string? Element { get; set; }
                public string? Avatar { get; set; }

      public bool Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
              public string? Username { get; set; }
              public string Password{get; set;}

        public string? PhoneNumber { get; set; }

        public string Role { get; set; }



        
    }
    public class CreateUpdateUserRequest
    {
        public string FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Role { get; set; }
    }
}
//       ,[]
//       ,[NormalizedUserName]
//       ,[NormalizedEmail]
//       ,[EmailConfirmed]
//       ,[PasswordHash]
//       ,[SecurityStamp]
//       ,[ConcurrencyStamp]
//       ,[PhoneNumber]
//       ,[PhoneNumberConfirmed]
//       ,[TwoFactorEnabled]
//       ,[LockoutEnd]
//       ,[LockoutEnabled]
//       ,[AccessFailedCount]
//   FROM [KoiFishDB].[dbo].[Users]
