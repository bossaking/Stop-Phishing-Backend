using System.Collections;
using System.Collections.Generic;

namespace Stop_Phishing.DTO.Course
{
    public class AllCoursesResponse
    {
        public IEnumerable<SimpleCourse> Courses { get; set; } 
    }
}