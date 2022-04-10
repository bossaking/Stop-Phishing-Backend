using System.Collections.Generic;

namespace Stop_Phishing.DTO.Test
{
    public class CreateQuestionRequest
    {
        public int? ImageNumber { get; set; }
        public string Title { get; set; }
        public string RightAnswerCommunicate { get; set; }
        public string BadAnswerCommunicate { get; set; }
        public ICollection<CreateAnswerRequest> Answers { get; set; }
    }
}