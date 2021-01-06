using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using WebAPI.Models;
using System.IO;
using System.Text;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get()
        {
            string query = @"
                             SELECT DepartmentID, DepartmentName FROM dbo.Department";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

            SqlDataReader myReader;

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

            return new JsonResult(table);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Department dep)
        {
            string query = @"
                             INSERT INTO dbo.Department VALUES
                             ('" + dep.DepartmentName + @"')
                            ";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

            SqlDataReader myReader;

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

            var filepath = "C:\\Users\\shipr\\Desktop\\Result\\department.txt";

            using (StreamWriter writer = new StreamWriter(filepath, append: true))
            {
                foreach (var item in dep.DepartmentName)
                {
                    writer.Write(item.ToString());
                }
                writer.WriteLine(" ");

            }

            return new JsonResult("Added successfully");
        }

    }
}
