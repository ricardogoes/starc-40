using System;

namespace STARC.Domain.ViewModels.Customers
{
    public class CustomerToQueryViewModel
    {
        public long CustomerId { get; set; }

        public string Name { get; set; }

        public string DocumentId { get; set; }

        public string Address { get; set; }

        public bool Status { get; set; }

        public long CreatedBy { get; set; }

        public string CreatedName { get; set; }

        public DateTime CreatedDate { get; set; }

        public long LastUpdatedBy { get; set; }

        public string LastUpdatedName { get; set; }

        public DateTime LastUpdatedDate { get; set; }
    }
}
