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
    public class CarrierController : ApiController
    {
        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            string query = @"
                select * from ( select carrierid, carriername, ROW_NUMBER() 
                OVER(Partition by carriername order by carrierid desc) rn from [TQL].[dbo].[tblCarrier] 
                where CarrierName <> '' and CarrierName <> ' ()') a 
                order by carrierid desc
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
        public DataTable Post(Carrier carrier)
        {
            try
            {
                string query = @"
                    select distinct top(50) CarrierId, 
                    CarrierName from [TQL].[dbo].[tblCarrier] 
                    where CarrierName LIKE ('%" + carrier.carriername+ @"%')
                    order by CarrierName asc
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