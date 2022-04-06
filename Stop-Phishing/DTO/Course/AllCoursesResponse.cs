using System.Collections;
using System.Collections.Generic;

namespace Stop_Phishing.DTO.Course
{
    public class AllCoursesResponse
    {
        public IEnumerable<Models.Course> Courses { get; set; } 
    }
}