using Microsoft.AspNetCore.Mvc;
using ZoobookTest.Service.Employee;
using ZoobookTest.Service.Model;

namespace ZoobookTest.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        [HttpGet(Name = "GetEmployees")]
        public IEnumerable<EmployeeDto> GetAll()
        {
            return _employeeService.GetAll();
        }


        [HttpGet(Name = "GetEmployee")]
        public IActionResult Get(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var employee = _employeeService.Get(id.Value);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }


        [HttpPost(Name = "CreateEmployee")]
        public IActionResult Create([FromBody] EmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _employeeService.Add(employeeDto);
            return StatusCode(201, result);
        }

        [HttpPut(Name = "EditEmployee")]
        public IActionResult Edit([FromBody] EmployeeDto employeeDto)
        {
            try
            {
                _employeeService.Update(employeeDto);
                return StatusCode(200);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpDelete("RemoveEmployee")]
        public IActionResult Remove(int id)
        {
            try
            {
                _employeeService.Delete(id);
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }
    }
}