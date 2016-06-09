using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplicationCS
{
    class LearnLINQ
    {
        List<Types.Employee> empls = new List<Types.Employee>();
        List<int> mngrs = new List<int> { 1, 2 };
        List<string> mngrNames = new List<string> { "Sergey Turin", "Sergey Balog" };
        public void TestLinq()
        {
            PrepareData();
            Test();
            Console.WriteLine("Welcome to demo");
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
        private void Test()
        {
            //TestJoin();
            //TestGroupJoin();
            //TestGroupBy();
            //TestZip();
            TestToLookup();
        }
        private void TestJoin()
        {
            empls.Join(empls, e => e.ManagerID, m => m.ID, (e, m) => new { Title = e.Title, Manager = m.Title }).ToList().ForEach(x=> Console.WriteLine("Employee: " + x.Title + " Manager: " + x.Manager));
            //Dictionary<int, string> dict = empls.ToDictionary(x => x.ID, x => x.Title);
        }
        private void TestGroupJoin()
        {
            empls.GroupJoin(empls, m => m.ID, e => e.ManagerID, (m, e) => new Types.EmployeesGroupResult{ Title = m.Title, Employees = e }).OrderByDescending(x=>x.Employees.Count()).ToList().ForEach(x => Console.WriteLine(x));
        }
        private void TestGroupBy()
        {
            empls.GroupBy(e => new { e.DepartmentID, e.Age }).Select(grp => new { Department = grp.Key, Employees = grp.Count(), AvgAge = grp.Average(x => x.Age) }).ToList().ForEach(x => Console.WriteLine(x.Department + " has " + x.Employees + " employees with average age = " + x.AvgAge));
        }
        private void TestZip()
        {
            mngrs.Zip(mngrNames, (x, y) => x + " " + y).ToList().ForEach(x=> Console.WriteLine(x));
        }
        private void TestToLookup()
        {
            ILookup<int, Types.Employee> ageLookup = empls.ToLookup(x => x.Age);
            ILookup<string, Types.Employee> depLookup = empls.ToLookup(x => x.DepartmentTitle);
            foreach(var grp in ageLookup)
            {
                Console.WriteLine("Age " + grp.Key + " (" + grp.Count() + ")");
                foreach(Types.Employee empl in grp)
                {
                    Console.WriteLine("\t" + empl.Title);
                }
            }
            foreach (var grp in depLookup)
            {
                Console.WriteLine("Department " + grp.Key + " (" + grp.Count() + ")");
                foreach (Types.Employee empl in grp)
                {
                    Console.WriteLine("\t" + empl.Title);
                }
            }
        }
        #region Prepare data
        private void PrepareData()
        {
            empls.Add(new Types.Employee {ID = 1, Age = 36, Title = "Sergey Balog", DepartmentID = 1, DepartmentTitle = "Executive", ManagerID = 2 });
            empls.Add(new Types.Employee { ID = 2, Age = 37, Title = "Sergey Trurin", DepartmentTitle = "Executive", DepartmentID = 1});
            empls.Add(new Types.Employee { ID = 3, Age = 25, Title = "Olga Loseva", DepartmentID = 2, DepartmentTitle = "Support", ManagerID = 1 });
            empls.Add(new Types.Employee { ID = 4, Age = 25, Title = "Rosina Chushkina", DepartmentID = 3, DepartmentTitle = "QA", ManagerID = 1 });
        }
        #endregion


    }
}
