using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stop_Phishing.DAL.Interfaces.Services;
using Stop_Phishing.DTO.Course;
using Stop_Phishing.Services;

namespace Stop_Phishing.Controllers
{
    [ApiController]
    [Route("course/")]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ILogger<CourseService> _logger;

        public CourseController(ICourseService courseService, ILogger<CourseService> logger)
        {
            _courseService = courseService;
            _logger = logger;
        }

        [HttpGet("courses/")]
        public async Task<IActionResult> GetAllCoursesAsync()
        {
            return Ok(await _courseService.GetAllCoursesAsync());
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCourseByIdAsync(Guid id)
        {
            return Ok(await _courseService.GetCourseByIdAsync(id));
        }

        [HttpPost("create/")]
        public async Task<IActionResult> CreateCourseAsync([FromBody] CreateCourseRequest courseRequest)
        {
            var result = await _courseService.CreateCourseAsync(courseRequest);
            if (result.Status)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpPut("update/{id:guid}")]
        public async Task<IActionResult> UpdateCourseAsync([FromBody] UpdateCourseRequest courseRequest, Guid id)
        {
            var result = await _courseService.UpdateCourseAsync(courseRequest, id);
            if (result.Status)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpDelete("delete/{id:guid}")]
        public async Task<IActionResult> RemoveCourseAsync(Guid id)
        {
            var result = await _courseService.RemoveCourseAsync(id);
            if (result.Status)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}