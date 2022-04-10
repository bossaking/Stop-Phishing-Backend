using System;
using System.Collections;
using System.Collections.Generic;
using Stop_Phishing.DTO.Course;

namespace Stop_Phishing.DTO.Test
{
    public class TestResponse
    {
        public Guid Id { get; set; }
        public IEnumerable<SimpleQuestion> Questions { get; set; }
    }
}