using ZoobookTest.DataAccess.Data;
using ZoobookTest.Domain.Employee;

namespace ZoobookTest.DataAccess.Repository
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private ApplicationDbContext _context;
        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
