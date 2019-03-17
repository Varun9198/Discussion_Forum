using System.Collections;
using System.Collections.Generic;

namespace DiscFor.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string QuestionString { get; set; }
        public virtual User User1 { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}