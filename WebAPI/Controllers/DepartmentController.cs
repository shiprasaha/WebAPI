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

        [HttpPost]
        public IActionResult Post([FromBody] Department dep)
        {
            var dirParameter = AppDomain.CurrentDomain.BaseDirectory + @"\file.txt";
            var filepath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            List<string> department = new List<string>();

            using (StreamWriter writer = new StreamWriter(filepath + "\\Result\\department.txt", append: true))
            {
                writer.Write("Department ID :" + dep.DepartmentID.ToString() + " ");
                writer.Write("Department Name :" + dep.DepartmentName.ToString() + " ");
                writer.Write("\r\n");

                department.Add(dep.DepartmentName.ToString());
            }

            return new JsonResult("Added successfully");
        }
    }
}
