using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Stop_Phishing.DTO.Test
{
    public class CreateTestRequest
    {
        public ICollection<IFormFile> Images { get; set; }
        public ICollection<CreateQuestionRequest> Questions { get; set; }
    }
}