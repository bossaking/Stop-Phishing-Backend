using System;
using System.Collections;
using System.Collections.Generic;

namespace Stop_Phishing.DTO.Course
{
    public class UpdateCourseRequest
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<CreateLessonRequest> CreatedLessons { get; set; }
        public IEnumerable<SimpleLesson> UpdatedLessons { get; set; }
        public IEnumerable<Guid> DeletedLessonsIds { get; set; }
    }
}