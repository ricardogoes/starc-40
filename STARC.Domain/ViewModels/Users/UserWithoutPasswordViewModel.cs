using System;

namespace STARC.Domain.ViewModels.Users
{
    public class UserWithoutPasswordViewModel
    {
        public long UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }
        
        public string Email { get; set; }

        public bool Status { get; set; }

        public int? UserProfileId { get; set; }

        public long CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public long LastUpdatedBy { get; set; }

        public DateTime LastUpdatedDate { get; set; }

        public long? CustomerId { get; set; }
    }
}
