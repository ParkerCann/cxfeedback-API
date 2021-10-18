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
    public class QuestionController : ApiController
    {
        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            string query = @"
                SELECT [questionid]
                      ,[question_name]
                      ,[question_source]
                      ,[productid]
                  FROM [TQL_UX].[dbo].[Question]
                ";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["FeedbackDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        // POST 
        public DataTable Post(Question question)
        {
            try
            {
                string query = @"
                    SELECT [questionid]
                      ,[question_name]
                      ,[question_source]
                      ,[productid]
                  FROM [TQL_UX].[dbo].[Question]
                  where question_name like ('%" + question.question_name + @"%')
                    ";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["FeedbackDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return table;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}