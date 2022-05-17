using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {   
        //for dependency injection to access configuration from appsettings
        private readonly IConfiguration _configuration;
        //for dependency injection to get the application path 
        private readonly IWebHostEnvironment _environment;
        public EmployeeController(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select EmployeeId, EmployeeName, Department, convert(varchar(10), DateOfJoining,120) as DateOfJoining, PhotoFileName from dbo.Employee";
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
        public JsonResult Post(Employee employee)
        {
            string query = @"insert into dbo.Employee 
                (EmployeeName,Department,DateOfJoining,PhotoFileName)
                values 
                ('" + employee.EmployeeName + @"',
                '" + employee.Department + @"',
                '" + employee.DateOfJoining + @"',
                '" + employee.PhotoFileName + @"'
                )";
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
        public JsonResult Put(Employee employee)
        {
            string query = @"update dbo.Employee set 
            EmployeeName = '" + employee.EmployeeName + @"',
            Department = '" + employee.Department + @"',
            DateOfJoining = '" + employee.DateOfJoining + @"',
            PhotoFileName = '" + employee.PhotoFileName + @"'
            where EmployeeId = " + employee.EmployeeId+ @" 
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
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"delete from dbo.Employee
            where EmployeeId = " + id + @" 
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
        //custom route name
        [Route ("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                //extract the first file attched in the request body
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var physicalPath = _environment.ContentRootPath + "/Photos/" + fileName;
                //save file in Photos folder
                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                return new JsonResult(fileName);
            }
            catch(Exception)
            {
                //default filename
                return new JsonResult("anonymous.png");
            }
        }
        //get all names 
        [Route("GetAllDepartmentsNames")]
        [HttpGet]
        public JsonResult GetAllDepartmentsNames()
        {
            string query = @"select Department from dbo.Employee";
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
    }
}
