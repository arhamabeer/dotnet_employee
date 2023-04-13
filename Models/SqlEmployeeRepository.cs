namespace dotnet_mvc.Models
{
    public class SqlEmployeeRepository : IEmployee_Repository
    {
        private readonly AppDbContext context;

        public SqlEmployeeRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Employee AddNewEmpoyee(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
            return employee;
        }

        public Employee DeleteEmplyee(int id)
        {
            Employee _emp = context.Employees.Find(id);
            if(_emp != null )
            {
                context.Employees.Remove(_emp); 
                context.SaveChanges();
            }
            return _emp;
        }

        //public IEnumerable<Employee> getEmployees()
        //{
        //    throw new NotImplementedException();
        //}

        public List<Employee> getEmployees()
        {
            return context.Employees.ToList();

        }

        public ResponseEmployeeRepository getName(int id)
        {
            ResponseEmployeeRepository _rer;
            Employee _emp = context.Employees.Find(id);
            if(_emp != null)
            {
                _rer = new ResponseEmployeeRepository(_emp, true, "Employee found.");
                return _rer;
            }
            else
            {
                return  new ResponseEmployeeRepository(null, false, "Employee not found.");
            }
        }

        public Employee UpdateEmplyee(Employee employee)
        {
            var _emp = context.Employees.Attach(employee);
            _emp.State = EntityState.Modified;
            context.SaveChanges();
            return employee;
        }
    }
}
