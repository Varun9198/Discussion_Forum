using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DiscFor.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}