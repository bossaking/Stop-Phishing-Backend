using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stop_Phishing.DAL.Interfaces.Services;
using Stop_Phishing.DTO.Course;
using Stop_Phishing.DTO.Test;
using Stop_Phishing.Services;

namespace Stop_Phishing.Controllers
{
    [ApiController]
    [Route("course/test/")]
    public class TestController : Controller
    {
        private readonly ITestService _testService;
        private readonly ILogger<TestService> _logger;

        public TestController(ITestService testService, ILogger<TestService> logger)
        {
            _testService = testService;
            _logger = logger;
        }

        // [HttpGet("courses/")]
        // public async Task<IActionResult> GetAllCoursesAsync()
        // {
        //     return Ok(await _courseService.GetAllCoursesAsync());
        // }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetTestByCourseIdAsync(Guid id)
        {
            return Ok(await _testService.GetTestByCourseIdAsync(id));
        }
        
        [HttpGet("test/{id:guid}")]
        public async Task<IActionResult> GetTestByIdAsync(Guid id)
        {
            return Ok(await _testService.GetTestByIdAsync(id));
        }


        [HttpPost("create/{id:guid}")]
        public async Task<IActionResult> CreateTestAsync([FromForm] CreateTestRequest testRequest, Guid id)
        {
            var result = await _testService.CreateTestAsync(testRequest, id);
            if (result.Status)
            {
                return Ok(result);
            }
        
            return BadRequest(result);
        }
        
        [HttpPut("update/{id:guid}")]
        public async Task<IActionResult> UpdateTestAsync([FromForm] UpdateTestRequest testRequest, Guid id)
        {
            var result = await _testService.UpdateTestAsync(testRequest, id);
            if (result.Status)
            {
                return Ok(result);
            }
        
            return BadRequest(result);
        }
        
        [HttpDelete("delete/{id:guid}")]
        public async Task<IActionResult> RemoveTestAsync(Guid id)
        {
            var result = await _testService.RemoveTestAsync(id);
            if (result.Status)
            {
                return Ok(result);
            }
        
            return BadRequest(result);
        }
    }
}