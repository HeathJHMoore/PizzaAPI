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
    public class ValuesController : ApiController
    {
        // GET api/values
        public HttpResponseMessage Get()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PizzaAppDB"].ConnectionString;

            SqlConnection sqlConnection = new SqlConnection(connectionString);

            string query = @"SELECT * FROM dbo.Department";

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable employeeTable = new DataTable();

            using (sqlDataAdapter)
            {
                sqlCommand.CommandType = CommandType.Text;

                sqlDataAdapter.Fill(employeeTable);
            }


                return Request.CreateResponse(HttpStatusCode.OK, employeeTable);
        }

        //POST METHOD
        public HttpResponseMessage POST(Department dep)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["PizzaAppDB"].ConnectionString;

                SqlConnection sqlConnection = new SqlConnection(connectionString);

                string query = @"INSERT INTO dbo.Department OUTPUT inserted.* VALUES ('" + dep.DepartmentName + "' )";

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


        // GET api/values/5
        

        
        

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
