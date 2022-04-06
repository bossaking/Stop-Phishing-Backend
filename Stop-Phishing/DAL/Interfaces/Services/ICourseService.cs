using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stop_Phishing.DTO.Course;
using Stop_Phishing.Helpers.Token;
using Stop_Phishing.Models;

namespace Stop_Phishing.DAL.Interfaces.Services
{
    public interface ICourseService
    {
        public Task<IEnumerable<Course>> GetAllCoursesAsync();
        public Task<Course> GetCourseByIdAsync(Guid id);
        public Task<ResultMessage> CreateCourseAsync(CreateCourseRequest courseRequest);
    }
}