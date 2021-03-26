using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using API.Models;
namespace API.Controllers
{
    
    public class OrderPizzaController : ApiController
    {
        [Route("api/orderpizza/{orderID}")]
        public HttpResponseMessage GET(int orderID)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["PizzaAppDB"].ConnectionString;

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                string query = "SELECT * FROM dbo.FoodOrderPizza WHERE OrderID = " + orderID.ToString() + ";";

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

        [HttpPost]
        public HttpResponseMessage POST(FoodOrderPizza FOP)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["PizzaAppDB"].ConnectionString;

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                string query = @"exec spFoodOrderPizza_PostFoodOrderPizza @PizzaID = "+FOP.PizzaID+", @OrderID = "+FOP.OrderID+", @OrderItemIndex= "+FOP.OrderItemIndex+";";

                

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