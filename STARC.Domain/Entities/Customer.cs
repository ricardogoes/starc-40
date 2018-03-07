using System;

namespace STARC.Domain.Entities
{
    public class Customer
    {
        public long CustomerId { get; set; }

        public string Name { get; set; }

        public string DocumentId { get; set; }

        public string Address { get; set; }

        public bool Status { get; set; }

        public long CreatedBy { get; set; }        

        public DateTime CreatedDate { get; set; }

        public long LastUpdatedBy { get; set; }

        public DateTime LastUpdatedDate { get; set; }
        
        public void ChangeStatus()
        {
            this.Status = this.Status ? false : true;
        }
    }
}
