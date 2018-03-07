using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using STARC.Api.FiltersAttributes;
using STARC.Api.Models;
using STARC.Domain.Entities;
using STARC.Domain.Interfaces.AppServices;
using STARC.Domain.ViewModels.TestPlan;
using System;

namespace STARC.Api.Controllers
{
    [ValidateModel]
    [HasAuthority]
    [Authorize("Bearer")]
    [Produces("application/json")]
    [Route("api/TestPlans")]
    public class TestPlansController : BaseController
    {
        private readonly ITestPlanAppService __testPlanApp;

        public TestPlansController(ITestPlanAppService testPlanApp, IUserAppService userApp, ILogger<TestPlansController> logger)
            :base(userApp, logger)
        {
            __testPlanApp = testPlanApp;
        }

        [HttpGet("ByProject/{projectId}", Name = "GetTestPlansByProject")]
        public IActionResult GetByProject(long projectId)
        {
            try
            {
                return Ok(new ApiResponse(ApiResponseState.Success, __testPlanApp.GetByProject(projectId)));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpGet("Active/ByProject/{projectId}", Name = "GetActiveTestPlansByProject")]
        public IActionResult GetActiveByProject(long projectId)
        {
            try
            {
                return Ok(new ApiResponse(ApiResponseState.Success, __testPlanApp.GetActiveByProject(projectId)));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpGet("{id}", Name = "GetTestPlanById")]
        public IActionResult Get(long id)
        {
            try
            {
                var searchedTestPlan = __testPlanApp.GetById(id);

                if (searchedTestPlan == null)
                    return NotFound(new ApiResponse(ApiResponseState.Failed, "Test Plan not Found"));

                return Ok(new ApiResponse(ApiResponseState.Success, searchedTestPlan));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpGet("{id}/Structure", Name = "GetTestPlanStructure")]
        public IActionResult GetStructure(long id)
        {
            //TODO: Add tests
            try
            {
                var searchedTestPlan = __testPlanApp.GetById(id);

                if (searchedTestPlan == null)
                    return NotFound(new ApiResponse(ApiResponseState.Failed, "Test Plan not Found"));

                return Ok(new ApiResponse(ApiResponseState.Success, __testPlanApp.GetStructure(id)));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPost(Name = "InsertTestPlan")]
        public IActionResult Insert([FromBody]TestPlanToInsertViewModel testPlanToInsert)
        {
            try
            {
                if (testPlanToInsert == null)
                    return BadRequest(new ApiResponse(ApiResponseState.Failed, "Test Plan is null"));

                var testPlan = Mapper.Map<TestPlanToInsertViewModel, TestPlan>(testPlanToInsert);
                testPlan.Status = true;
                testPlan.CreatedBy = LoggedUser.UserId;
                testPlan.CreatedDate = DateTime.Now;
                testPlan.LastUpdatedBy = LoggedUser.UserId;
                testPlan.LastUpdatedDate = DateTime.Now;

                var isValidEntity = __testPlanApp.IsValid(testPlan);

                if (isValidEntity.Status == false)
                {
                    return BadRequest(new ApiResponse(ApiResponseState.Failed, "Validation failed, please see details", isValidEntity.ValidationMessages));
                }

                var testPlanIdInserted = __testPlanApp.Add(testPlan);

                return CreatedAtRoute("GetTestPlanById", new { controller = "TestPlans", id = testPlanIdInserted }, null);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPut("{id}", Name = "UpdateTestPlan")]
        public IActionResult Update(long id, [FromBody]TestPlanToUpdateViewModel testPlanToUpdate)
        {
            try
            {
                if (testPlanToUpdate == null || testPlanToUpdate.TestPlanId != id)
                    return BadRequest(new ApiResponse(ApiResponseState.Failed, "Invalid request"));

                var searchedTestPlan = __testPlanApp.GetById(id);
                if (searchedTestPlan == null)
                    return NotFound(new ApiResponse(ApiResponseState.Failed, "Test Plan Not Found"));

                var testPlan = Mapper.Map<TestPlanToUpdateViewModel, TestPlan>(testPlanToUpdate);
                testPlan.Status = searchedTestPlan.Status;
                testPlan.CreatedBy = searchedTestPlan.CreatedBy;
                testPlan.CreatedDate = searchedTestPlan.CreatedDate;
                testPlan.LastUpdatedBy = LoggedUser.UserId;
                testPlan.LastUpdatedDate = DateTime.Now;

                var isValidEntity = __testPlanApp.IsValid(testPlan);

                if (isValidEntity.Status == false)
                {
                    return BadRequest(new ApiResponse(ApiResponseState.Failed, "Validation failed, please see details", isValidEntity.ValidationMessages));
                }

                __testPlanApp.Update(testPlan);

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPut("{id}/ChangeStatus", Name = "ChangeStatusTestPlan")]
        public IActionResult ChangeStatus(long id)
        {
            try
            {
                var searchedTestPlan = Mapper.Map<TestPlan>(__testPlanApp.GetById(id));

                if (searchedTestPlan == null)
                    return NotFound(new ApiResponse(ApiResponseState.Failed, "Test Plan Not Found"));

                searchedTestPlan.ChangeStatus();
                searchedTestPlan.LastUpdatedBy = LoggedUser.UserId;
                searchedTestPlan.LastUpdatedDate = DateTime.Now;

                __testPlanApp.ChangeStatus(searchedTestPlan);

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}