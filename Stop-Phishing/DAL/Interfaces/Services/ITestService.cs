using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stop_Phishing.DTO.Course;
using Stop_Phishing.DTO.Test;
using Stop_Phishing.Helpers.Token;
using Stop_Phishing.Models;

namespace Stop_Phishing.DAL.Interfaces.Services
{
    public interface ITestService
    {
        public Task<TestResponse> GetTestByCourseIdAsync(Guid id);
        public Task<TestResponse> GetTestByIdAsync(Guid id);
        public Task<ResultMessage> CreateTestAsync(CreateTestRequest testRequest, Guid courseId);
        public Task<ResultMessage> RemoveTestAsync(Guid id);
    }
}