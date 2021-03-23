using BisontechDemo.DataAccessModels;
using BisontechDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BisontechDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        // GET: api/<EmployeesController>
        //        northwndserver.database.windows.net

        //northwndadmin
        //Rolando$fcfm2021
        //Rolando.fcfm2021

        //Server= myServerAddress; Database=myDataBase;User Id = myUsername; password=myPassword;Trusted_Connection=False;MultipleActiveResultSets=true;

        //https://stackoverflow.com/questions/47399666/entity-framework-scaffold-dbcontext-login-failed-for-user/47401662#47401662?newreg=cdd97bd8d0aa43f88f12b3a615e96a20
        //Scaffold-DbContext "Server=northwndserver.database.windows.net;Database=NorthwndCloud;User Id=northwndadmin;password=Rolando`$fcfm2021;Trusted_Connection=False;MultipleActiveResultSets=true;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir DataAccess
        //Scaffold-DbContext "Server=northwndserver.database.windows.net;Database=NorthwndCloud;User Id=northwndadmin;password=Rolando.fcfm2021;Trusted_Connection=False;MultipleActiveResultSets=true;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir DataAccess
        //[HttpGet]
        //public List<Employee> Get()
        //{
        //    DataAccessModels.NorthwndCloudContext dc = new DataAccessModels.NorthwndCloudContext();
        //    return dc.Employees.ToList();

        //}

        DataAccessModels.NorthwndCloudContext dc = new DataAccessModels.NorthwndCloudContext();

        [HttpGet]
        public IActionResult Get()
        {

            return Ok(dc.Employees.Select(s => new { s.FirstName, s.LastName, s.TitleOfCourtesy, s.PostalCode, s.HomePhone, s.EmployeeId }).ToList());

        }

        // GET api/<EmployeesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var employee = dc.Employees.Where(w => w.EmployeeId == id).FirstOrDefault();

            if (employee == null)
                throw new Exception($"User with id {id} not found in database");


            return Ok(employee);
        }

        // POST api/<EmployeesController>
        [HttpPost]
        public IActionResult Post([FromBody] EmployeeDTO employeeDTO)
        {

            try
            {
                var newEmployeeRegister = new Employee();
                newEmployeeRegister.FirstName = employeeDTO.Name;
                newEmployeeRegister.LastName = employeeDTO.LastName;

                dc.Employees.Add(newEmployeeRegister);
                dc.SaveChanges();
                dc.Entry(newEmployeeRegister);
                return Ok(new { newEmployeeRegister.EmployeeId });
            }
            catch (Exception ex)
            {
                var msg = ex.Message + (ex.InnerException != null ? (ex.InnerException) : "");

                return StatusCode(StatusCodes.Status500InternalServerError
                    , new { errorMessage = "No se pudo insertar el registro. Detalles: " + ex.Message });

            }


        }

        // PUT api/<EmployeesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EmployeesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
