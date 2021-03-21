using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace API.Controllers
{
    public class PizzaController : ApiController
    {
        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PizzaAppDB"].ConnectionString;

            SqlConnection sqlConnection = new SqlConnection(connectionString);

            string query = "SELECT * From dbo.Pizza";

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

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}