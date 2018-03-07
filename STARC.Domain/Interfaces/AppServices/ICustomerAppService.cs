using STARC.Domain.Entities;
using STARC.Domain.Models;
using STARC.Domain.ViewModels.Customers;
using System.Collections.Generic;

namespace STARC.Domain.Interfaces.AppServices
{
    public interface ICustomerAppService
    {
        long Add(Customer customer);

        void Update(Customer customer);

        void ChangeStatus(Customer customer);

        CustomerToQueryViewModel GetById(long customerId);

        IEnumerable<CustomerToQueryViewModel> GetAll();        

        IEnumerable<CustomerToQueryViewModel> GetActive();

        CustomerToQueryViewModel GetByDocumentId(string documentId);

        bool IsDocumentIdUnique(string documentId);

        EntityValidationResult IsValid(Customer customer);
    }
}
