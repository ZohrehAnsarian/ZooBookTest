using System.Linq.Expressions;
using ZoobookTest.Service.Model;

namespace ZoobookTest.Service.Employee
{
    public interface IEmployeeService
    {
        EmployeeDto Add(EmployeeDto entity);
        void Update(EmployeeDto entity);
        void Delete(int id);
        EmployeeDto Get(int id);
        IEnumerable<EmployeeDto> GetAll();
    }
}
