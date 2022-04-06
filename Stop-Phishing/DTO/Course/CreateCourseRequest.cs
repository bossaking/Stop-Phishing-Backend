using System.ComponentModel.DataAnnotations;

namespace Stop_Phishing.DTO.Course
{
    public class CreateCourseRequest
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        
    }
}