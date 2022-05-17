using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using WebAPI.Models;
namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        //here we use dependency injection to access the appsettings file
        private readonly IConfiguration _configuration;
        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //api method
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select DepartmentId, DepartmentName from dbo.Department";
            DataTable table = new DataTable();
            // store the connection string here
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppConnection");
            SqlDataReader myReader;
            //to execute query and fill the results into a data table using the sql connection and the sql command
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }

            }
            //return the data table as json result
            return new JsonResult(table);
        }
        [HttpPost]
        public JsonResult Post(Department department)
        {
            string query = @"insert into dbo.Department values 
                ('" + department.DepartmentName + @"')";
            DataTable table = new DataTable();
            // store the connection string here
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppConnection");
            SqlDataReader myReader;
            //to execute query and fill the results into a data table using the sql connection and the sql command
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }

            }
            //return the data table as json result
            return new JsonResult("Added");
        }
        [HttpPut]
        public JsonResult Put(Department department)
        {
            string query = @"update dbo.Department set 
            DepartmentName = '" + department.DepartmentName + @"'
            where DepartmentId = " + department.DepartmentId + @" 
            ";
            DataTable table = new DataTable();
            // store the connection string here
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppConnection");
            SqlDataReader myReader;
            //to execute query and fill the results into a data table using the sql connection and the sql command
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }

            }
            //return the data table as json result
            return new JsonResult("Updated");
        }
        [HttpDelete ("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"delete from dbo.Department
            where DepartmentId = " + id + @" 
            ";
            DataTable table = new DataTable();
            // store the connection string here
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppConnection");
            SqlDataReader myReader;
            //to execute query and fill the results into a data table using the sql connection and the sql command
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }

            }
            //return the data table as json result
            return new JsonResult("Deleted");
        }
    }
}
