using lamda_practice.Data;
using System;
using System.Globalization;
using System.Linq;

namespace lambda_practice
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var ctx = new DatabaseContext())
            {
                //1. Listar todos los empleados cuyo departamento tenga una sede en Chihuahua
                 var query1 = ctx.Employees
                     .Where(e => e.City.Name == "Chihuahua")
                     .Select(s => new { s.FirstName , s.LastName, s.City });

                 foreach (var employee in query1)
                 {
                     Console.WriteLine("{0}  {1} \t city:{2}",
                         employee.FirstName, employee.LastName, employee.City.Name);
                 }
                 //2. Listar todos los departamentos y el numero de empleados que pertenezcan a cada departamento.
                 var query2 = ctx.Employees
                     .GroupBy(e => e.Department.Name)
                     .Select(s => new { name = s.Key , countDep = s.Count() });

                 foreach (var dept in query2)
                 {
                     Console.WriteLine("Department = {0} \t Count = {1}",
                         dept.name,
                         dept.countDep);
                 }
                //3. Listar todos los empleados remotos. Estos son los empleados cuya ciudad no se encuentre entre las sedes de su departamento.

                var query3 = ctx.Employees
                    .Where(e => e.Department.Cities.Any(s => s.Name == e.City.Name))
                    .Distinct()
                    .Select(s => new { s.FirstName });

                foreach (var employee in query3)
                {
                    Console.WriteLine("Remote: {0}",
                        employee.FirstName);
                }
                //4. Listar todos los empleados cuyo aniversario de contratación sea el próximo mes.
                var query4 = ctx.Employees
                    .Where(e => e.HireDate.Month == 4);

                foreach (var date in query4)
                {
                    Console.WriteLine("Name: {0} date: {1:d} ",
                        date.FirstName, date.HireDate);
                }

                //Listar los 12 meses del año y el numero de empleados contratados por cada mes.
                var query5 = ctx.Employees
                       .GroupBy(e => e.HireDate.Month)
                       .OrderBy(s => s.Key)
                       .Select(s => new { date = s.Key ,count = s.Count() });

                foreach (var date in query5)
                {
                    Console.WriteLine("Month: {0}  No:{1} ",
                        date.date, date.count);
                }

            }


            Console.Read();
        }
    }
}
