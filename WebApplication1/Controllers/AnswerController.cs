using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;


namespace WebApplication1.Controllers
{
    public class AnswerController : ApiController
    {
        // POST 
        public string Post(Answer answer)
        {
            try
            {
                string query = @"
                insert into TQL_UX.dbo.Answer(feedbackid, questionid, review, rating) values
                (('" + answer.feedbackid + @"'),
                ('" + answer.questionid + @"'),
                ('" + answer.review + @"'),
                ('" + answer.rating + @"')); 

                SELECT SCOPE_IDENTITY();
                ";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["FeedbackDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                {
                    con.Open();

                    int modified = Convert.ToInt32(cmd.ExecuteScalar());

                    if (con.State == System.Data.ConnectionState.Open) con.Close();
                    return Convert.ToString(modified);
                }
                //using (var da = new SqlDataAdapter(cmd))
                //{
                //    cmd.CommandType = CommandType.Text;
                //    da.Fill(table);
                //}

                //return ("Added Successfully!");



            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}