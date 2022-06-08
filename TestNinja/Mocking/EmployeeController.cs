using System.Data.Entity;
using TestNinja.Mocking.EmployeeControllers;

namespace TestNinja.Mocking
{
    public class EmployeeController
    {
        private EmployeeContext _db;
        private IEmployeeStorage _employeeStorage;

        public EmployeeController(IEmployeeStorage employeeStorage = null)
        {
            _db = new EmployeeContext();
            _employeeStorage = employeeStorage ?? new EmployeeStorage();
        }

        public ActionResult DeleteEmployee(int id)
        {
            var employee = _db.Employees.Find(id);
            _db.Employees.Remove(employee);
            _db.SaveChanges();
            return RedirectToAction("Employees");
        }

        public ActionResult DeleteEmployeeRefactored(int id)
        {
            _employeeStorage.DeleteEmployee(id);
            return RedirectToAction("Employees");
        }

        private ActionResult RedirectToAction(string employees)
        {
            return new RedirectResult();
        }
    }

    public class ActionResult { }
 
    public class RedirectResult : ActionResult { }
    
    public class EmployeeContext
    {
        public DbSet<Employee> Employees { get; set; }

        public void SaveChanges()
        {
        }
    }

    public class Employee
    {
    }
}