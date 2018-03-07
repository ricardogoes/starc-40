using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using STARC.Api.FiltersAttributes;
using STARC.Api.Models;
using STARC.Domain.Entities;
using STARC.Domain.Interfaces.AppServices;
using STARC.Domain.ViewModels.TestCase;
using System;

namespace STARC.Api.Controllers
{
    [ValidateModel]
    [HasAuthority]
    [Authorize("Bearer")]
    [Produces("application/json")]
    [Route("api/TestCases")]
    public class TestCasesController : BaseController
    {
        private readonly ITestCaseAppService __app;

        public TestCasesController(ITestCaseAppService app, IUserAppService userApp, ILogger<TestPlansController> logger)
            :base(userApp, logger)
        {
            __app = app;
        }

        [HttpGet("ByTestPlan/{testPlanId}", Name = "GetTestCasesByTestPlan")]
        public IActionResult GetByTestPlan(long testPlanId)
        {
            try
            {
                return Ok(new ApiResponse(ApiResponseState.Success, __app.GetByTestPlan(testPlanId)));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpGet("ByTestSuite/{testSuiteId}", Name = "GetTestCasesByTestSuite")]
        public IActionResult GetByTestSuite(long testSuiteId)
        {
            //TODO: Add Tests
            try
            {
                return Ok(new ApiResponse(ApiResponseState.Success, __app.GetByTestSuite(testSuiteId)));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpGet("{id}", Name = "GetTestCaseById")]
        public IActionResult Get(long id)
        {
            try
            {
                var searchedTestCase = __app.GetById(id);

                if (searchedTestCase == null)
                    return NotFound(new ApiResponse(ApiResponseState.Failed, "Test Case not Found"));

                return Ok(new ApiResponse(ApiResponseState.Success, searchedTestCase));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPost(Name = "InsertTestCase")]
        public IActionResult Insert([FromBody]TestCaseToInsertViewModel testCaseToInsert)
        {
            try
            {
                if (testCaseToInsert == null)
                    return BadRequest(new ApiResponse(ApiResponseState.Failed, "Test Case is null"));

                var testCase = Mapper.Map<TestCaseToInsertViewModel, TestCase>(testCaseToInsert);
                testCase.Status = true;
                testCase.CreatedBy = LoggedUser.UserId;
                testCase.CreatedDate = DateTime.Now;
                testCase.LastUpdatedBy = LoggedUser.UserId;
                testCase.LastUpdatedDate = DateTime.Now;

                var isValidEntity = __app.IsValid(testCase);

                if (isValidEntity.Status == false)
                {
                    return BadRequest(new ApiResponse(ApiResponseState.Failed, "Validation failed, please see details", isValidEntity.ValidationMessages));
                }

                var testCaseIdInserted = __app.Add(testCase);

                return CreatedAtRoute("GetTestCaseById", new { controller = "TestCases", id = testCaseIdInserted }, null);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPut("{id}", Name = "UpdateTestCase")]
        public IActionResult Update(long id, [FromBody]TestCaseToUpdateViewModel testCaseToUpdate)
        {
            try
            {
                if (testCaseToUpdate == null || testCaseToUpdate.TestCaseId != id)
                    return BadRequest(new ApiResponse(ApiResponseState.Failed, "Invalid request"));

                var searchedTestCase = __app.GetById(id);

                if (searchedTestCase == null)
                    return NotFound(new ApiResponse(ApiResponseState.Failed, "Test Case Not Found"));

                var testCase = Mapper.Map<TestCase>(testCaseToUpdate);
                testCase.Status = searchedTestCase.Status;
                testCase.CreatedBy = searchedTestCase.CreatedBy;
                testCase.CreatedDate = searchedTestCase.CreatedDate;
                testCase.LastUpdatedBy = LoggedUser.UserId;
                testCase.LastUpdatedDate = DateTime.Now;

                var isValidEntity = __app.IsValid(testCase);

                if (isValidEntity.Status == false)
                {
                    return BadRequest(new ApiResponse(ApiResponseState.Failed, "Validation failed, please see details", isValidEntity.ValidationMessages));
                }

                __app.Update(testCase);

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPut("{id}/ChangeStatus", Name = "ChangeStatusTestCase")]
        public IActionResult ChangeStatus(long id)
        {
            try
            {
                var searchedTestCase = Mapper.Map<TestCase>(__app.GetById(id));

                if (searchedTestCase == null)
                    return NotFound(new ApiResponse(ApiResponseState.Failed, "Test Case Not Found"));

                searchedTestCase.ChangeStatus();
                searchedTestCase.LastUpdatedBy = LoggedUser.UserId;
                searchedTestCase.LastUpdatedDate = DateTime.Now;

                __app.ChangeStatus(searchedTestCase);

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}