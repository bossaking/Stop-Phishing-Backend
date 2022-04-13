using System;

namespace Stop_Phishing.DTO.Test
{
    public class UpdateAnswerRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsRight { get; set; }
    }
}