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
        public List<Employee> Get()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PizzaAppDB"].ConnectionString;

            SqlConnection sqlConnection = new SqlConnection(connectionString);

            string query = @"SELECT * FROM dbo.Employee";

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable employeeTable = new DataTable();

            using (sqlDataAdapter)
            {
                sqlCommand.CommandType = CommandType.Text;

                sqlDataAdapter.Fill(employeeTable);
            }

            
            List<Employee> employeeList = new List<Employee>();

            employeeList = (from DataRow dr in employeeTable.Rows
                            orderby dr["EmployeeId"]
                            select new Employee()
                            {
                                EmployeeId = Convert.ToInt32(dr["EmployeeId"]),
                                EmployeeName = dr["EmployeeName"].ToString(),
                                Department = dr["Department"].ToString()
                            }).ToList();

            


                return employeeList;
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
