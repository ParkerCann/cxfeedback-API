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
    public class CustomerController : ApiController
    {
        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            string query = @"
                SELECT distinct [CustomerId] ,[CustomerName], [CustomerStatus]  FROM [TQL].[dbo].[tblCustomers]  
                where CustomerStatus <> '3' and   CustomerStatus <> '7' and  CustomerStatus <> '15' and  CustomerStatus <> '17' and  CustomerStatus <> '20' and  CustomerName <> ''  
                order by CustomerName asc
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
        public DataTable Post(Customer customer)
        {
            try
            {
                string query = @"
                    select distinct top(50) CustomerId, 
                    CustomerName from [TQL].[dbo].[tblCustomers] 
                    where CustomerName LIKE ('%"+customer.customername+@"%')
                    order by CustomerName asc
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