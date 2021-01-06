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

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult Get()
        {
            string query = @"
                              select EmployeeId, EmployeeName, Department,
                              convert(varchar(10),DateOfJoining,120) as DateOfJoining
                              from dbo.Employee";

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
        public ActionResult Post(Employee emp)
        {
            //string query = @"
            //                  insert into dbo.Employee 
            //                  (EmployeeId,EmployeeName,Department,DateOfJoining)
            //                  values 
            //                  (
            //                  '" + emp.EmployeeId + @"'
            //                  '" + emp.EmployeeName + @"'
            //                  ,'" + emp.Department + @"'
            //                  ,'" + emp.DateOfJoining + @"'
            //                  )
            //                  ";

            //DataTable table = new DataTable();

            //string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

            //SqlDataReader myReader;

            //using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            //{
            //    myCon.Open();
            //    using (SqlCommand myCommand = new SqlCommand(query, myCon))
            //    {
            //        myReader = myCommand.ExecuteReader();
            //        table.Load(myReader);

            //        myReader.Close();
            //        myCon.Close();
            //    }
            //}

            using (StreamWriter writer = new StreamWriter("C:\\Users\\shipr\\Desktop\\Result\\text.txt", append: true))
            {
                //foreach (var item in emp.EmployeeName)
                //    writer.Write("Employee Name :" + item.ToString() + " ");
                //foreach (var item in emp.Department)
                //    writer.Write("Employee Department :" + item.ToString()+ " ");

                writer.Write("Employee ID :" + emp.EmployeeId.ToString() + " ");
                writer.Write("Employee Name :" + emp.EmployeeName.ToString() + " ");
                writer.Write("Employee Department :" + emp.Department.ToString() + " ");
                writer.Write("Employee DOJ :" + emp.DateOfJoining.ToString() + " ");


            }

            return new JsonResult("Added successfully");
        }


        [Route("GetAllDepartmentNames")]
        public JsonResult GetAllDepartmentNames()
        {
            string query = @"
                    select DepartmentName from dbo.Department
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
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            var departmentList = new string[] {"Class A" , "Class B"};

            return new JsonResult(departmentList);
        }
    }
}
