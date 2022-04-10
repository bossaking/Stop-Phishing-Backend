using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Stop_Phishing.Models
{
    public class Test
    {
        [Key] 
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public virtual Course Course { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}