using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplicationCS.Types
{
    class Employee
    {
        public string Title { get; set; }
        public int Age { get; set; }
        public int ID { get; set; }
        public int ManagerID { get; set; }
        public int DepartmentID { get; set; }
    }
    class EmployeesGroupResult
    {
        public string Title { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
        public override string ToString()
        {
            string res = string.Empty;
            if (Employees.Count() == 0) {
                return res;
            }
            res = "Manager: " + Title + "\n";
            res += "_______________________________________________________\n";
            Employees.ToList().ForEach(x=> res+= x.Title + "\n");
            return res;
        }
    }
}
