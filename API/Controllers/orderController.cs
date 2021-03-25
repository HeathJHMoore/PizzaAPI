using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using API.Models;

namespace API.Controllers
{
    
    public class orderController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage POST()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["PizzaAppDB"].ConnectionString;

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                string query = @"INSERT INTO dbo.FoodOrder OUTPUT inserted.orderID VALUES (CURRENT_TIMESTAMP)";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                DataTable idTable = new DataTable();

                using (sqlDataAdapter)
                {
                    sqlCommand.CommandType = CommandType.Text;

                    sqlDataAdapter.Fill(idTable);
                }

                return Request.CreateResponse(HttpStatusCode.OK, idTable);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
       
}
