using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Feedback
    {
        public int feedbackid { get; set; }
        public int respondeeid { get; set; }
        public int productid { get; set; }
        public int feedbacktypeid { get; set; }
        public int conductor_id { get; set; }
        public TimeSpan callduration { get; set; }
        public DateTime datecompleted { get; set; }
        public int respondeetypeid { get; set; }
        public DateTime datesubmitted { get; set; }
    }
}