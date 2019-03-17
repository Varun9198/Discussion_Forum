namespace DiscFor.Models
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public string AnswerString { get; set; }
        public virtual User CurrentUser { get; set; }
        public virtual Question CurrentQuestion { get; set; }
    }
}