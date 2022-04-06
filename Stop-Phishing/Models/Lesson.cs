

using System;
using System.ComponentModel.DataAnnotations;

namespace Stop_Phishing.Models
{
    public class Lesson
    {
        [Key] 
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid CourseId { get; set; }
        public virtual Course Course { get; set; }
    }
}