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
    //[RoutePrefix("orderpizza")]
    public class OrderPizzaController : ApiController
    {
        public string Get()
        {
            return "value";
        }

        [HttpPost]
        public HttpResponseMessage POST(FoodOrderPizza FOP)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["PizzaAppDB"].ConnectionString;

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                string query = @"exec spFoodOrderPizza_PostFoodOrderPizza @PizzaID = "+FOP.PizzaID+", @OrderID = "+FOP.OrderID+";";

                

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