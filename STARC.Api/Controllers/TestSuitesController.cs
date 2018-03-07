using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using STARC.Api.FiltersAttributes;
using STARC.Api.Models;
using STARC.Domain.Entities;
using STARC.Domain.Interfaces.AppServices;
using STARC.Domain.ViewModels.TestSuite;
using System;

namespace STARC.Api.Controllers
{
    [ValidateModel]
    [HasAuthority]
    [Authorize("Bearer")]
    [Produces("application/json")]
    [Route("api/TestSuites")]
    public class TestSuitesController : BaseController
    {
        private readonly ITestSuiteAppService __app;

        public TestSuitesController(ITestSuiteAppService app, IUserAppService userApp, ILogger<TestPlansController> logger)
            :base(userApp, logger)
        {
            __app = app;
        }

        [HttpGet("{id}", Name = "GetTestSuiteById")]
        public IActionResult Get(long id)
        {
            try
            {
                var searchedTestSuite = __app.GetById(id);

                if (searchedTestSuite == null)
                    return NotFound(new ApiResponse(ApiResponseState.Failed, "Test Suite not Found"));

                return Ok(new ApiResponse(ApiResponseState.Success, searchedTestSuite));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpGet("ByTestPlan/{testPlanId}", Name = "GetTestSuiteByTestPlan")]
        public IActionResult GetByTestPlan(long testPlanId)
        {
            // TODO: Add tests
            try
            {
                return Ok(new ApiResponse(ApiResponseState.Success, __app.GetByTestPlan(testPlanId)));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpGet("ByParentTestSuite/{parentTestSuiteId}", Name = "GetTestSuiteByParentTestSuite")]
        public IActionResult GetByParentTestSuite(long parentTestSuiteId)
        {
            // TODO: Add tests
            try
            {
                return Ok(new ApiResponse(ApiResponseState.Success, __app.GetByParentTestSuite(parentTestSuiteId)));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPost(Name = "InsertTestSuite")]
        public IActionResult Insert([FromBody]TestSuiteToInsertViewModel testSuiteToInsert)
        {
            try
            {
                if (testSuiteToInsert == null)
                    return BadRequest(new ApiResponse(ApiResponseState.Failed, "Test Suite is null"));

                var testSuite = Mapper.Map<TestSuiteToInsertViewModel, TestSuite>(testSuiteToInsert);
                testSuite.CreatedBy = LoggedUser.UserId;
                testSuite.CreatedDate = DateTime.Now;
                
                var isValidEntity = __app.IsValid(testSuite);

                if (isValidEntity.Status == false)
                {
                    return BadRequest(new ApiResponse(ApiResponseState.Failed, "Validation failed, please see details", isValidEntity.ValidationMessages));
                }

                var testSuiteIdInserted = __app.Add(testSuite);

                return CreatedAtRoute("GetTestSuiteById", new { controller = "TestSuites", id = testSuiteIdInserted }, null);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPut("{id}", Name = "UpdateTestSuite")]
        public IActionResult Update(long id, [FromBody]TestSuiteToUpdateViewModel testSuiteToUpdate)
        {
            try
            {
                if (testSuiteToUpdate == null || testSuiteToUpdate.TestSuiteId != id)
                    return BadRequest(new ApiResponse(ApiResponseState.Failed, "Invalid request"));

                var searchedTestSuite = __app.GetById(id);

                if (searchedTestSuite == null)
                    return NotFound(new ApiResponse(ApiResponseState.Failed, "Test Suite Not Found"));

                var testSuite = Mapper.Map<TestSuite>(testSuiteToUpdate);
                testSuite.CreatedBy = searchedTestSuite.CreatedBy;
                testSuite.CreatedDate = searchedTestSuite.CreatedDate;
                
                var isValidEntity = __app.IsValid(testSuite);

                if (isValidEntity.Status == false)
                {
                    return BadRequest(new ApiResponse(ApiResponseState.Failed, "Validation failed, please see details", isValidEntity.ValidationMessages));
                }

                __app.Update(testSuite);

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpDelete("{id}", Name = "DeleteTestSuite")]
        public IActionResult Delete(long id)
        {
            try
            {
                var searchedTestSuite = Mapper.Map<TestSuite>(__app.GetById(id));

                if (searchedTestSuite == null)
                    return NotFound(new ApiResponse(ApiResponseState.Failed, "Test Suite Not Found"));

                __app.Delete(id);

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}