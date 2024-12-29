using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        // get all employee
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var AllEmployees = dbContext.Employees.ToList();

            return Ok(AllEmployees);
        }
        // add new employee
        [HttpPost]
        public IActionResult AddEmployees(AddEmployeeDto addempdto)
        {
            var empEntity = new Employee()
            {
                Name = addempdto.Name,
                Email = addempdto.Email,
                Phone = addempdto.Phone,
                Salary = addempdto.Salary,
            };
            dbContext.Employees.Add(empEntity);
            dbContext.SaveChanges();
            return Ok(empEntity);
            
        }
        // Fetch single employee using id (route name and param name are same)
        [HttpGet]
        [Route("{empid:int}")]
        public IActionResult GetEmployeesById(int empid)
        {
           var id = dbContext.Employees.Find(empid);
            if (id == null)
            {
                return NotFound();
            }
            else {
                return Ok(id);
                 }
        }

        // update employee data
        [HttpPut]
        [Route("{empid:int}")]
        public IActionResult updateEmployee(int empid,UpdateEmployeeDto updatedto)
        {
            var emp = dbContext.Employees.Find(empid);
            if (emp == null)
            {
                return NotFound();
            }
            emp.Name = updatedto.Name;
            emp.Email = updatedto.Email;
            emp.Phone = updatedto.Phone;
            emp.Salary = updatedto.Salary;

           
            dbContext.SaveChanges();
            return Ok(emp);
        }
        [HttpDelete]
        [Route("{empid:int}")]
        public IActionResult DeleteEmployeesById(int empid)
        {
            var id = dbContext.Employees.Find(empid);
            if (id == null)
            {
                return NotFound();
            }
            dbContext.Employees.Remove(id);
            dbContext.SaveChanges();
            return Ok(id);
        }
    }
}
