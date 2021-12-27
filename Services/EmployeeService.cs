using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pract2.Models;

namespace Pract2.Services
{
    public class EmployeeService
    {

       //// Create a List of Employee
       //List<Employee> AllEmployees = new List<Employee>
       //{
       //     new Employee {Id = "EA01",NRIC = "S1111111D",Name = "May Tan",Gender="F",
       //         BirthDate=DateTime.Parse("11/11/1980"), Salary=3000,Department="IT"},
       //     new Employee {Id = "EA02",NRIC = "S1212121A",Name = "John Lim",Gender="M",
       //         BirthDate=DateTime.Parse("01/11/1981"), Salary=4000,Department="HR" },
       //     new Employee {Id = "EA03",NRIC = "S1313131B",Name = "Joann Tan",Gender="F",
       //         BirthDate=DateTime.Parse("11/11/1980"), Salary=4000,Department="IT"},
       //     new Employee {Id = "EA04",NRIC = "S1234567D",Name = "Peter Ang",Gender="M",
       //         BirthDate=DateTime.Parse("01/11/1981"), Salary=5000,Department="HR" },
       // };

        private Models.HRDbContext _context;
        public EmployeeService(Models.HRDbContext context)
        {
            _context = context;
        }

        public bool AddEmployee(Employee newemployee)
        {
            if (EmployeeExists(newemployee.Id))
            {
                return false;
            }
            _context.Add(newemployee);
            _context.SaveChanges();
            return true;
        }         
        public List<Employee> GetAllEmployees()
        {
            List<Employee> AllEmployees = new List<Employee>();
            AllEmployees= _context.Employees.ToList();
            return AllEmployees;
        }
        public  Employee GetEmployeeById(String id)
        {
            //List<Employee> AllEmployees = new List<Employee>();

            //Employee employee = null;
            //foreach (Employee item in AllEmployees)
            //{
            //    if (item.Id == id)
            //    {
            //        employee = item;
            //    }
            //}
            Employee theEmployee =  _context.Employees.Where(e => e.Id == id).FirstOrDefault();
            return theEmployee;    
         }
        private bool EmployeeExists(string id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }

        public bool UpdateEmployee(Employee theemployee)
        {
            bool updated = true;
            _context.Attach(theemployee).State = EntityState.Modified;

            try
            {
                 _context.SaveChanges();
                updated = true;
                 
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(theemployee.Id))
                {
                    updated= false;
                }
                else
                {
                    throw;
                }
            }
            return updated;


        }

    }
}
