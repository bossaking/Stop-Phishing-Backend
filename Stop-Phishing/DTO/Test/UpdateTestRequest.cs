using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Stop_Phishing.DAL.Interfaces.Services;

namespace Stop_Phishing.DTO.Test
{
    public class UpdateTestRequest
    {
        public ICollection<IFormFile> Images { get; set; }
        public ICollection<Guid> DeletedQuestions { get; set; }
        public ICollection<CreateQuestionRequest> NewQuestions { get; set; }
        public ICollection<UpdateQuestionRequest> EditedQuestions { get; set; }
    }
}