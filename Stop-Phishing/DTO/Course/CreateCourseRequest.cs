using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Stop_Phishing.Models;

namespace Stop_Phishing.DTO.Course
{
    public class CreateCourseRequest
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<SimpleLesson> Lessons { get; set; }
    }
}