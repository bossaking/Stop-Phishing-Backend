using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Stop_Phishing.DTO.Test;

namespace Stop_Phishing.Models
{
    public class Question
    {
        [Key] 
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string RightAnswerCommunicate { get; set; }
        public string BadAnswerCommunicate { get; set; }
        public string Image { get; set; }
        public Guid TestId { get; set; }
        public virtual Test Test { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}