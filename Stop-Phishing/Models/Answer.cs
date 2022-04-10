using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Stop_Phishing.Models
{
    public class Answer
    {
        [Key] public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsRight { get; set; }
        public Guid QuestionId { get; set; }
        public virtual Question Question { get; set; }
    }
}