using System;
using System.Collections.Generic;

namespace Stop_Phishing.DTO.Test
{
    public class UpdateQuestionRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public string RightAnswerCommunicate { get; set; }
        public string BadAnswerCommunicate { get; set; }
        public ICollection<CreateAnswerRequest> AddedAnswers { get; set; }
        public ICollection<UpdateAnswerRequest> EditedAnswers { get; set; }
        public ICollection<Guid> DeletedAnswers { get; set; }
    }
}