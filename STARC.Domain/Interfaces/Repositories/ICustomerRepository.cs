using STARC.Domain.Entities;
using STARC.Domain.Models;
using STARC.Domain.ViewModels.Customers;
using System.Collections.Generic;

namespace STARC.Domain.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        long Add(Customer customer);

        void Update(Customer customer);

        void ChangeStatus(Customer customer);

        CustomerToQueryViewModel GetById(long customerId);

        IEnumerable<CustomerToQueryViewModel> GetAll();

        IEnumerable<CustomerToQueryViewModel> GetActive();

        CustomerToQueryViewModel GetByDocumentId(string documentId);
    }
}
