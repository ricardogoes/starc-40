using STARC.Application.Common;
using STARC.Domain.Entities;
using STARC.Domain.Interfaces.AppServices;
using STARC.Domain.Interfaces.Repositories;
using STARC.Domain.Models;
using STARC.Domain.ViewModels.Customers;
using System;
using System.Collections.Generic;

namespace STARC.Application
{
    public class CustomerAppService: ICustomerAppService
    {
        private readonly ICustomerRepository __repository;

        public CustomerAppService(ICustomerRepository repository)
        {
            __repository = repository;
        }

        public long Add(Customer customer)
        {
            try
            {
                return __repository.Add(customer);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(Customer customer)
        {
            try
            {
                __repository.Update(customer);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void ChangeStatus(Customer customer)
        {
            try
            {
                __repository.ChangeStatus(customer);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<CustomerToQueryViewModel> GetActive()
        {
            try
            {
                return __repository.GetActive();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<CustomerToQueryViewModel> GetAll()
        {
            try
            {
                return __repository.GetAll();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public CustomerToQueryViewModel GetById(long customerId)
        {
            try
            {
                return __repository.GetById(customerId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public CustomerToQueryViewModel GetByDocumentId(string documentId)
        {
            try
            {
                if (string.IsNullOrEmpty(documentId))
                    throw new ArgumentException("documentId invalid");

                return __repository.GetByDocumentId(documentId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool IsDocumentIdUnique(string documentId)
        {
            try
            {
                if (string.IsNullOrEmpty(documentId))
                    throw new ArgumentException("documentId invalid");

                var customer = GetByDocumentId(documentId);

                if (customer == null)
                    return true;

                return false;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public EntityValidationResult IsValid(Customer customer)
        {
            try
            {
                if (customer == null)
                    throw new ArgumentException("Customer invalid");

                var validation = new EntityValidationResult();

                validation.Status = true;
                validation.ValidationMessages = new List<string>();

                if (!CpfCnpjService.IsCpfCnpj(customer.DocumentId))
                {
                    validation.Status = false;
                    validation.ValidationMessages.Add("CPF/CNPJ invalid");
                }

                if (customer.CustomerId == 0 && !IsDocumentIdUnique(customer.DocumentId))
                {
                    validation.Status = false;
                    validation.ValidationMessages.Add("CPF/CNPJ already exists");
                }

                return validation;
            }
            catch (Exception)
            {
                throw;
            }
        }       
    }
}
