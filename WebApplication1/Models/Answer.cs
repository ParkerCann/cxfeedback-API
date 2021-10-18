using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Answer
    {
        public int rowid { get; set; }
        public int feedbackid { get; set; }
        public int questionid { get; set; }
        public string review { get; set; }
        public int rating { get; set; }
    }
}