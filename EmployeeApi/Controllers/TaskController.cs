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
    public class TaskController : ApiController
    {
        // Task data storage
        private TaskRepository taskRepository;

        public TaskController()
        {
            this.taskRepository = new TaskRepository();
        }

        // Read all tasks
        public IEnumerable<EmployeeTask> Get()
        {
            return taskRepository.GetAll();
        }

        // Read a task by task ID
        public IHttpActionResult Get(int id)
        {
            EmployeeTask task = taskRepository.Get(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        // Create a task
        public IHttpActionResult PostEmployee(EmployeeTask task)
        {
            task = taskRepository.Add(task);
            return Ok(task);
        }

        // Update a task
        public IHttpActionResult PutEmployee(EmployeeTask task)
        {
            if (!taskRepository.Update(task))
            {
                return NotFound();
            }
            return Ok(task);
        }

        // Delete a task
        public IHttpActionResult Delete(int id)
        {
            EmployeeTask emp = taskRepository.Get(id);
            if (emp == null)
            {
                return NotFound();
            }
            taskRepository.Remove(id);
            return Ok();
        }
    }
}
