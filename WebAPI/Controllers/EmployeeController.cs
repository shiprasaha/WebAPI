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

        [HttpPost]
        public ActionResult Post(Employee emp)
        {
            var filepath = "C:\\Users\\shipr\\Desktop\\Result\\employeeData.txt";
            List<String> lines = new List<string>(System.IO.File.ReadAllLines(filepath));

            using (StreamWriter writer = new StreamWriter(filepath, append: true)) {
                writer.Write("Employee ID :" + emp.EmployeeId.ToString() + " ");
                writer.Write("Employee Name :" + emp.EmployeeName.ToString() + " ");
                writer.Write("Employee Department :" + emp.Department.ToString() + " ");
                writer.Write("Employee DOJ :" + emp.DateOfJoining.ToString() + " ");
                writer.Write("\r\n");
            }

            lines.Add(" ");
            return new JsonResult("Added successfully");
        }

        [Route("GetAllDepartmentNames")]
        public JsonResult GetAllDepartmentNames()
        {
            var departmentList = new string[] {"Finance" , "IT", "Management"};
            return new JsonResult(departmentList);
        }
    }
}
