using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Question
    {
        public int questionid { get; set; }
        public string question_name { get; set; }
        public string question_source { get; set; }
        public int productid { get; set; }
    }
}