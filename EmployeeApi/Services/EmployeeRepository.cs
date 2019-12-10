using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeeApi.Models;

// Services for employee data access
namespace EmployeeApi.Services
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee Get(int id);
        Employee Add(Employee emp);
        void Remove(int id);
        bool Update(Employee emp);
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        private static List<Employee> employees = new List<Employee>();
        private static int _nextId = 1;

        public IEnumerable<Employee> GetAll()
        {
            return employees;
        }

        public Employee Get(int id)
        {
            return employees.Find(p => p.EmployeeId == id);
        }

        public void Remove(int id)
        {
            employees.RemoveAll(p => p.EmployeeId == id);
        }

        public Employee Add(Employee emp)
        {
            if (emp == null)
            {
                throw new ArgumentNullException("emp");
            }
            emp.EmployeeId = _nextId++;
            employees.Add(emp);
            return emp;
        }

        public bool Update(Employee emp)
        {
            if (emp == null)
            {
                throw new ArgumentNullException("emp");
            }
            int index = employees.FindIndex(p => p.EmployeeId == emp.EmployeeId);
            if (index == -1)
            {
                return false;
            }
            //employees[index] = emp;
            employees.RemoveAt(index);
            employees.Add(emp);
            return true;
        }
    }
}