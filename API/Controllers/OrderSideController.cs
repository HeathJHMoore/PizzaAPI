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
    public class OrderSideController : ApiController
    {
        public HttpResponseMessage POST(FoodOrderSide FOS)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["PizzaAppDB"].ConnectionString;

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                string query = @"exec spFoodOrderSide_PostFoodOrderSide @SideID = " + FOS.SideID + ", @OrderID = " + FOS.OrderID + ";";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                DataTable dataTable = new DataTable();

                using (sqlDataAdapter)
                {

                    sqlCommand.CommandType = CommandType.Text;
                    sqlDataAdapter.Fill(dataTable);
                }

                return Request.CreateResponse(HttpStatusCode.OK, dataTable);

            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    } 
    
}
