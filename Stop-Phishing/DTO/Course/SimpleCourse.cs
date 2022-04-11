using System;
using System.Collections.Generic;
using Stop_Phishing.Models;

namespace Stop_Phishing.DTO.Course
{
    public class SimpleCourse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
        public Guid TestId { get; set; }
        public virtual IEnumerable<SimpleLesson> Lessons { get; set; }
    }
}