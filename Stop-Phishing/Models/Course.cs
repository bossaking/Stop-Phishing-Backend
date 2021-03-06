using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Stop_Phishing.Models
{
    public class Course
    {
        [Key] 
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
        public virtual IEnumerable<Lesson> Lessons { get; set; }
        public Guid TestId { get; set; }
        public virtual Test Test { get; set; }
    }
}