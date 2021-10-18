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
    public class DetailedFeedbackController : ApiController
    {
        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            string query = @"
                select f.feedbackid,
	            RespondeeName =
            CASE f.respondeetypeid
	            WHEN '1' THEN 'None'
	            WHEN '2' THEN c.CarrierName
	            WHEN '3' THEN cu.CustomerName
	            WHEN '4' THEN CONCAT(p.FirstName,  ' ', p.LastName)
	            ELSE 'None'
            END,
            FORMAT (datecompleted, 'MM-dd-yyyy') as datecompleted,
            emp.Fullname,
            feedback_name,
            product_name,
            respondee_type,
            callduration,
            FORMAT (datesubmitted, 'MM-dd-yyyy, hh:mm:ss') as datesubmitted

            from 
	            tql_ux.dbo.tblEmployeeIDs emp,
	            tql_ux.dbo.FeedbackType ft,
	            tql_ux.dbo.Product pr,
	            tql_ux.dbo.RespondeeType rt,
	            tql_ux.dbo.Feedback f

            left join tql.dbo.tblCarrier c
            on c.Carrierid = f.respondeeid

            left join tql.dbo.tblCustomers cu
            on cu.CustomerId = f.respondeeid

            left join tql_hris.dbo.tPerson p
            on	p.WorkerID = f.respondeeid

            where emp.WorkerID = f.conductor_id
            and
            ft.feedbacktypeid = f.feedbacktypeid
            and
            pr.Productid = f.productid
            and
            rt.respondeetypeid = f.respondeetypeid

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
    }
}
