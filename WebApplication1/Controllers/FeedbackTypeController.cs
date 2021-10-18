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
    public class FeedbackTypeController : ApiController
    {
        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            string query = @"
                select feedbacktypeid, feedback_name, version from feedbacktype order by feedbacktypeid asc
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
        public string Post(FeedbackType feedback)
        {
            try
            {
                string query = @"
                insert into tql_ux.dbo.feedbacktype(feedback_name, version) values
                (('" +feedback.feedback_name+ @"'),
                ('" +feedback.version+ @"'))
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

                return "Added Successfully!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Put(FeedbackType feedback)
        {
            try
            {
                string query = @"
                update tql_ux.dbo.feedbacktype set feedback_name=
                '" + feedback.feedback_name + @"'
                where feedbacktypeid=" + feedback.feedbacktypeid + @"
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

                return "Updated Successfully!";
            }
            catch (Exception)
            {

                return "Failed to update!";
            }
        }
    }
}
