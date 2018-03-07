using System;

namespace STARC.Domain.Entities
{
    public class UsersInProjects
    {
        public long UserInProjectId { get; set; }

        public long UserId { get; set; }

        public long ProjectId { get; set; }

        public long CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
