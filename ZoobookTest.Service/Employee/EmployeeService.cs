using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZoobookTest.Domain.Employee;
using ZoobookTest.Service.Model;

namespace ZoobookTest.Service.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public EmployeeDto Add(EmployeeDto entity)
        {
            Domain.Employee.Employee employee = new Domain.Employee.Employee
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                MiddleName = entity.MiddleName
            };
            var result = _employeeRepository.Add(employee);
            return new EmployeeDto() { Id = result.Id, FirstName = result.FirstName, LastName = result.LastName, MiddleName = result.MiddleName };
        }

        public void Delete(int id)
        {
            var employee = _employeeRepository.Get(id);
            if (employee != null)
                _employeeRepository.Delete(employee);
            else
                throw new KeyNotFoundException();

        }

        public EmployeeDto Get(int id)
        {
            var employee = _employeeRepository.Get(id);
            if (employee != null)
                return new EmployeeDto()
                {
                    FirstName = employee.FirstName,
                    Id = employee.Id,
                    LastName = employee.LastName,
                    MiddleName = employee.MiddleName
                };
            else
                return null;
        }

        public IEnumerable<EmployeeDto> GetAll()
        {
            var employees = _employeeRepository.GetAll();
            foreach (var employee in employees)
            {
                yield return new EmployeeDto()
                {
                    FirstName = employee.FirstName,
                    Id = employee.Id,
                    MiddleName = employee.MiddleName,
                    LastName = employee.LastName
                };
            }
        }

        public void Update(EmployeeDto entity)
        {
            var employee = _employeeRepository.Get(entity.Id);
            if (employee != null)
            {
                employee.FirstName = entity.FirstName;
                employee.LastName = entity.LastName;
                employee.MiddleName = entity.MiddleName;
                _employeeRepository.Update(employee);
            }
            else
                throw new KeyNotFoundException();
        }
    }
}
