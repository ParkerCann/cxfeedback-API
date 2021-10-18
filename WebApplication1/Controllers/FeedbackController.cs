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
    public class FeedbackController : ApiController
    {
        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            string query = @"
                SELECT [feedbackid]
                      ,[respondeeid]
                      ,[productid]
                      ,[feedbacktypeid]
                      ,[conductor_id]
                      ,[callduration]
                      ,[datecompleted]
                      ,[respondeetypeid]
                      ,[datesubmitted]
                  FROM [TQL_UX].[dbo].[Feedback]
                  order by feedbackid desc
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
        public string Post(Feedback feedback)
        {
            try
            {
                string query = @"
                insert into TQL_UX.dbo.Feedback(respondeeid, ProductID, FeedbackTypeID, conductor_id, callduration, DateCompleted, respondeetypeid, datesubmitted) values
                (('" + feedback.respondeeid+ @"'),
                ('" +feedback.productid+ @"'),
                ('" +feedback.feedbacktypeid+ @"'),
                ('" + feedback.conductor_id + @"'),
                ('" + feedback.callduration + @"'),
                ('" + feedback.datecompleted+ @"'),
                ('" +feedback.respondeetypeid+ @"'),
                ('" + feedback.datesubmitted+ @"')); 

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
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // PUT
        public string Put(Feedback feedback)
        {
            try
            {
                string query = @"
                update tql_ux.dbo.Feedback
                set
                    [respondeeid] = ('" + feedback.respondeeid + @"')
                    ,[productid] = ('" + feedback.productid + @"')
                    ,[feedbacktypeid] = ('" + feedback.feedbacktypeid + @"')
                    ,[conductor_id]=  ('" + feedback.conductor_id + @"')
                    ,[callduration]= ('" + feedback.callduration + @"')
                    ,[datecompleted]= ('" + feedback.datecompleted + @"')
                    ,[respondeetypeid]= ('" + feedback.respondeetypeid + @"')
                    ,[datesubmitted]= ('" + feedback.datesubmitted + @"')
                where feedbackid = ('" + feedback.feedbackid + @"');
                ";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["FeedbackDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                {
                    con.Open();

                    int modified = Convert.ToInt32(cmd.ExecuteScalar());

                    if (con.State == System.Data.ConnectionState.Open) con.Close();
                    return ("Successful");
                }



            }
            catch (Exception ex)
            {
                return ("Failed to update. Error: " + ex.Message);
            }
        }
    }
}

