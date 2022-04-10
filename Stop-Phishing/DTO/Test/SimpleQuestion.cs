using System;
using System.Collections;
using System.Collections.Generic;

namespace Stop_Phishing.DTO.Test
{
    public class SimpleQuestion
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string RightAnswerCommunicate { get; set; }
        public string BadAnswerCommunicate { get; set; }
        public string Image { get; set; }
        public IEnumerable<SimpleAnswer> Answers { get; set; }
    }
}