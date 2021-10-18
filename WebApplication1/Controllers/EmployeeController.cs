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
    public class EmployeeController : ApiController
    {
        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            string query = @"
                select distinct PersonID, CONCAT(FirstName, ' ', LastName) AS FullName from [TQL_HRIS].[dbo].[tPerson] order by FullName asc
                ";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["CarrierDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        // POST 
        public DataTable Post(EmployeeIDs employeeIDs)
        {
            try
            {
                string query = @"
                    select distinct top(50) FullName
                    from(
	                    select firstname, lastname, concat(firstname,' ',lastname) as FullName
	                    from TQL_HRIS.dbo.tPerson
	                    ) t
                    where fullname like ('%" + employeeIDs.fullname + @"%')
                    ";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["CarrierDB"].ConnectionString))
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