using ZoobookTest.Domain.Common;

namespace ZoobookTest.Domain.Employee
{
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }
}
