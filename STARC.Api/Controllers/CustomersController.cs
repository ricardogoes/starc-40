using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using STARC.Api.FiltersAttributes;
using STARC.Api.Models;
using STARC.Domain.Entities;
using STARC.Domain.Interfaces.AppServices;
using STARC.Domain.ViewModels.Customers;
using System;

namespace STARC.Api.Controllers
{
    [ValidateModel]
    [HasAuthority]
    [Authorize("Bearer")]
    [Produces("application/json")]
    [Route("api/Customers")]    
    public class CustomersController : BaseController
    {
        private readonly ICustomerAppService __customerApp;
                
        public CustomersController(ICustomerAppService customerApp, 
                                   IUserAppService userApp, 
                                   ILogger<CustomersController> logger)
            :base(userApp, logger)
        {
            __customerApp = customerApp;
        }

        [HttpGet("", Name = "GetAllCustomers")]
        public IActionResult GetAll()
        {
            try
            {
                var customers = __customerApp.GetAll();

                return Ok(new ApiResponse(ApiResponseState.Success, customers));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }            
        }

        [HttpGet("Active", Name = "GetActiveCustomers")]
        public IActionResult GetActive()
        {
            try
            {
                var customers = __customerApp.GetActive();

                return Ok(new ApiResponse(ApiResponseState.Success, customers));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }            
        }

        [HttpGet("{id}", Name = "GetCustomerById")]
        public IActionResult Get(long id)
        {
            try
            {
                var searchedCustomer = __customerApp.GetById(id);

                if (searchedCustomer == null)
                    return NotFound(new ApiResponse(ApiResponseState.Failed, "Customer not Found"));

                return Ok(new ApiResponse(ApiResponseState.Success, searchedCustomer));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }            
        }
        
        [HttpPost(Name = "InsertCustomer")]        
        public IActionResult Insert([FromBody]CustomerToInsertViewModel customerToInsert)
        {            
            try
            {
                if (customerToInsert == null)
                    return BadRequest(new ApiResponse(ApiResponseState.Failed, "Customer is null"));

                var customer = Mapper.Map<CustomerToInsertViewModel, Customer>(customerToInsert);
                customer.Status = true;
                customer.CreatedBy = 1;// LoggedUser.UserId;
                customer.CreatedDate = DateTime.Now;
                customer.LastUpdatedBy = 1;// LoggedUser.UserId;
                customer.LastUpdatedDate = DateTime.Now;

                var isValidCustomer = __customerApp.IsValid(customer);
                if (isValidCustomer.Status == false)
                {
                    return BadRequest(new ApiResponse(ApiResponseState.Failed, "Validation failed, please see details", isValidCustomer.ValidationMessages));
                }

                var customerIdInserted = __customerApp.Add(customer);

                return CreatedAtRoute("GetCustomerById", new { controller = "Customers", id = customerIdInserted }, null);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }            
        }
        
        [HttpPut("{id}", Name = "UpdateCustomer")]
        public IActionResult Update(long id, [FromBody]CustomerToUpdateViewModel customerToUpdate)
        {
            try
            {
                if (customerToUpdate == null || customerToUpdate.CustomerId != id)
                    return BadRequest(new ApiResponse(ApiResponseState.Failed, "Invalid request"));

                var searchedCustomer = __customerApp.GetById(id);
                if (searchedCustomer == null)
                    return NotFound(new ApiResponse(ApiResponseState.Failed, "Customer Not Found"));

                var customer = Mapper.Map<CustomerToUpdateViewModel, Customer>(customerToUpdate);
                customer.Status = searchedCustomer.Status;
                customer.CreatedBy = searchedCustomer.CreatedBy;
                customer.CreatedDate = searchedCustomer.CreatedDate;
                customer.LastUpdatedBy = 1;// LoggedUser.UserId;
                customer.LastUpdatedDate = DateTime.Now;

                var isValidCustomer = __customerApp.IsValid(customer);
                if (isValidCustomer.Status == false)
                    return BadRequest(new ApiResponse(ApiResponseState.Failed, "Validation failed, please see details", isValidCustomer.ValidationMessages));

                __customerApp.Update(customer);

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
        
        [HttpPut("{id}/ChangeStatus", Name = "CustomerChangeStatus")]
        public IActionResult ChangeStatus(long id)
        {
            try
            {
                var customer = Mapper.Map<Customer>(__customerApp.GetById(id));

                if (customer == null)
                    return NotFound(new ApiResponse(ApiResponseState.Failed, "Customer Not Found"));

                customer.ChangeStatus();
                customer.LastUpdatedBy = 1;// LoggedUser.UserId;
                customer.LastUpdatedDate = DateTime.Now;

                __customerApp.ChangeStatus(customer);

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}
