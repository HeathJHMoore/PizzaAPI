using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using API.Models;

namespace API.Controllers
{
    public class OrderDrinkController : ApiController
    {
        public HttpResponseMessage GET()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["PizzaAppDB"].ConnectionString;

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                string query = "SELECT * FROM dbo.FoodOrderDrink";

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
        
        [Route("api/orderdrink/{orderID}")]
        public HttpResponseMessage GET(int orderID)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["PizzaAppDB"].ConnectionString;

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                string query = "SELECT * FROM dbo.FoodOrderDrink WHERE OrderID = " + orderID.ToString() + ";";

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

        public HttpResponseMessage POST(FoodOrderDrink FOD)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["PizzaAppDB"].ConnectionString;

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                string query = @"exec spFoodOrderDrink_PostFoodOrderDrink @DrinkID = " + FOD.DrinkID + ", @OrderID = " + FOD.OrderID + ", @OrderItemIndex= "+FOD.OrderItemIndex+";";

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

