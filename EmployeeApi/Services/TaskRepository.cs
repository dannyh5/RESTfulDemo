using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeeApi.Models;

// Services for task data access
namespace EmployeeApi.Services
{
    public interface ITaskRepository
    {
        IEnumerable<EmployeeTask> GetAll();
        EmployeeTask Get(int taskid);
        EmployeeTask Add(EmployeeTask task);
        void Remove(int taskid);
        bool Update(EmployeeTask task);
    }

    public class TaskRepository : ITaskRepository
    {
        private static List<EmployeeTask> tasks = new List<EmployeeTask>();
        private static int _nextId = 1;

        public IEnumerable<EmployeeTask> GetAll()
        {
            return tasks;
        }

        public EmployeeTask Get(int id)
        {
            return tasks.Find(p => p.TaskId == id);
        }

        public void Remove(int id)
        {
            tasks.RemoveAll(p => p.TaskId == id);
        }

        public EmployeeTask Add(EmployeeTask task)
        {
            if (task == null)
            {
                throw new ArgumentNullException("task");
            }
            task.TaskId = _nextId++;
            tasks.Add(task);
            return task;
        }

        public bool Update(EmployeeTask task)
        {
            if (task == null)
            {
                throw new ArgumentNullException("task");
            }
            int index = tasks.FindIndex(p => p.TaskId == task.TaskId);
            if (index == -1)
            {
                return false;
            }
            //tasks[index] = task;
            tasks.RemoveAt(index);
            tasks.Add(task);
            return true;
        }
    }
}