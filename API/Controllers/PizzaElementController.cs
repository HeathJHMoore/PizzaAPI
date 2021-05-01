using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using API.Models;
namespace API.Controllers
{
    public class PizzaElementController : ApiController
    {
        // GET api/<controller>
        public HttpResponseMessage Get()
        {

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["PizzaAppDB"].ConnectionString;

                string query = "SELECT * FROM PizzaElement";

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                SqlDataAdapter DataAdapter = new SqlDataAdapter(sqlCommand);

                DataTable pizzaElementTable = new DataTable();

                using (DataAdapter)
                {
                    sqlCommand.CommandType = CommandType.Text;
                    DataAdapter.Fill(pizzaElementTable);
                }

                List<PizzaElement> pizzaElements = new List<PizzaElement>();

                pizzaElements = (from DataRow pizzaElement in pizzaElementTable.Rows
                                 select new PizzaElement()
                                 {
                                     id = Convert.ToInt32(pizzaElement["id"]),
                                     name = pizzaElement["name"].ToString(),
                                     price = Convert.ToDouble(pizzaElement["price"]),
                                     imgURL = pizzaElement["imgURL"].ToString(),
                                     type = pizzaElement["type"].ToString()
                                 }).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, pizzaElements);
            }

            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }


        }
        
    }
}