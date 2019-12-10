using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeApi.Models;
using EmployeeApi.Services;

namespace EmployeeApi.Controllers
{
    public class EmployeeController : ApiController
    {
        // Employee data storage
        private EmployeeRepository empRepository;

        public EmployeeController()
        {
            this.empRepository = new EmployeeRepository();
        }

        // Read all employees
        public IEnumerable<Employee> Get()
        {
            return empRepository.GetAll();
        }

        // Read an employee by employee ID
        public IHttpActionResult Get(int id)
        {
            Employee emp = empRepository.Get(id);
            if (emp == null)
            {
                return NotFound();
            }
            return Ok(emp);
        }

        // Create an employee
        public IHttpActionResult PostEmployee(Employee emp)
        {
            emp = empRepository.Add(emp);
            return Ok(emp);
        }

        // Update an employee
        public IHttpActionResult PutEmployee(Employee emp)
        {
            if (!empRepository.Update(emp))
            {
                return NotFound();
            }
            return Ok(emp);
        }

        //Delete an employee
        public IHttpActionResult Delete(int id)
        {
            Employee emp = empRepository.Get(id);
            if (emp == null)
            {
                return NotFound();
            }
            empRepository.Remove(id);
            return Ok();
        }
    }
}
