using System;
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
    }
}