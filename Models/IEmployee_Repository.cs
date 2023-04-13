namespace dotnet_mvc.Models
{
    public interface IEmployee_Repository
    {
        ResponseEmployeeRepository getName(int id);
        List<Employee> getEmployees();
        Employee AddNewEmpoyee (Employee employee);
        Employee UpdateEmplyee (Employee employee);
        Employee DeleteEmplyee (int id);
    }
}
