using Microsoft.AspNetCore.Mvc;
using ZoobookTest.Service.Employee;
using ZoobookTest.Service.Model;

namespace ZoobookTest.WebApi.Test
{
    public class EmployeeServiceFake : IEmployeeService
    {
        private readonly List<EmployeeDto> _employees;
        public EmployeeServiceFake()
        {
            _employees = new List<EmployeeDto>()
            {
                new EmployeeDto(){ Id=1, FirstName="Zoei",MiddleName="zi",LastName="Ansarian"},
                new EmployeeDto(){ Id=2, FirstName="Oliver",MiddleName="Mak",LastName="Maneman"},
                new EmployeeDto(){ Id=3, FirstName="William",MiddleName="",LastName="Damian"},
            };
        }
        public EmployeeDto Add(EmployeeDto entity)
        {
            entity.Id = _employees.Last().Id + 1;
            _employees.Add(entity);

            return new EmployeeDto { Id = entity.Id, FirstName = entity.FirstName, LastName = entity.LastName, MiddleName = entity.MiddleName };
        }

        public void Update(EmployeeDto entity)
        {

            var obj = _employees.Find(e => e.Id == entity.Id);
            obj.FirstName = entity.FirstName;
            obj.LastName = entity.LastName;
            obj.MiddleName = entity.MiddleName;

            _employees.Remove(entity);
            _employees.Add(obj);
        }

        public void Delete(int id)
        {
            var obj = _employees.Find(e => e.Id == id);
            if (obj != null)
                _employees.Remove(obj);
            else
                throw new Exception();

        }

        public EmployeeDto Get(int id)
        {
            return _employees.Where(e => e.Id == id).FirstOrDefault();
        }

        public IEnumerable<EmployeeDto> GetAll()
        {
            return _employees;
        }
    }
}
