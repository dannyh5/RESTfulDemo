using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

using EmployeeApi.Models;
using EmployeeApi.Controllers;
using EmployeeApi.Services;

namespace EmployeeApi.Test
{
    [TestFixture]
    public class TestCases
    {
        private EmployeeController empController = new EmployeeController();
        private EmployeeRepository empRepository = new EmployeeRepository();
        private TaskController taskController = new TaskController();
        private TaskRepository taskRepository = new TaskRepository();
        private List<int> taskList = new List<int>();

        [SetUp]
        public void DataSetup()
        {
            // Add some data to Employee and EmployeeTask before test
            EmployeeTask task = new EmployeeTask { TaskName = "Coding", StartDate = new DateTime(2019, 12, 10), DeadLine = new DateTime(2019, 12, 24) };
            task = taskRepository.Add(task);
            taskList.Add(task.TaskId);
            Employee emp = new Employee { LastName = "Wang", FirstName = "David", HiredDate = new DateTime(2019, 12, 08), TaskIdList = taskList };
            empRepository.Add(emp);
        }

        //Test Employee
        [Test]
        public void Test0EmployeeGetAll()
        {
            // Get data directly from Repository
            List<Employee> expected = (List<Employee>)empRepository.GetAll();
            // Call Get API
            List<Employee> actual = (List<Employee>)empController.Get();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test1EmployeeGet()
        {
            // Get data directly from Repository
            Employee expected = empRepository.Get(1);
            // Call Get by id API
            IHttpActionResult result = empController.Get(1);
            var actual = result as OkNegotiatedContentResult<Employee>;

            Assert.AreEqual(expected, actual.Content);
        }

        [Test]
        public void Test2EmployeePostEmployee()
        {
            Employee expected = new Employee { LastName = "Lin", FirstName = "Jason", HiredDate = new DateTime(2019, 12, 09), TaskIdList = null };
            // Call Post API
            empController.PostEmployee(expected);
            // Get data directly from Repository
            Employee actual = empRepository.Get(expected.EmployeeId);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test3EmployeePutEmployee()
        {
            Employee expected = new Employee { LastName = "Lin", FirstName = "Jason", HiredDate = new DateTime(2019, 12, 09), TaskIdList = null };
            expected = empRepository.Add(expected);
            // Update task list
            expected.TaskIdList = taskList;
            // Call Put API
            empController.PutEmployee(expected);
            // Get data directly from Repository
            Employee actual = empRepository.Get(expected.EmployeeId);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test4EmployeeDelete()
        {
            // Call Delete API
            empController.Delete(1);
            // Get data directly from Repository
            Employee actual = empRepository.Get(1);

            Assert.IsTrue(actual == null);
        }

        //Test Task
        [Test]
        public void Test5TaskGetAll()
        {
            // Get data directly from Repository
            List<EmployeeTask> expected = (List<EmployeeTask>)taskRepository.GetAll();
            // Call Get API
            List<EmployeeTask> actual = (List<EmployeeTask>)taskController.Get();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test6TaskGet()
        {
            // Get data directly from Repository
            EmployeeTask expected = taskRepository.Get(1);
            // Call Get by ID API
            IHttpActionResult result = taskController.Get(1);
            var actual = result as OkNegotiatedContentResult<EmployeeTask>;

            Assert.AreEqual(expected, actual.Content);
        }

        [Test]
        public void Test7PostTask()
        {
            EmployeeTask expected = new EmployeeTask { TaskName = "Testing", StartDate = new DateTime(2019, 12, 25), DeadLine = new DateTime(2020, 02, 04) };
            // Call Post API
            taskController.PostTask(expected);
            // Get data directly from Repository
            EmployeeTask actual = taskRepository.Get(expected.TaskId);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test8PutTask()
        {
            EmployeeTask expected = new EmployeeTask { TaskName = "Testing", StartDate = new DateTime(2019, 12, 25), DeadLine = new DateTime(2020, 02, 04) };
            expected = taskRepository.Add(expected);
            // Update Deadline
            expected.DeadLine = new DateTime(2020, 02, 14);
            // Call Put API
            taskController.PutTask(expected);
            // Get data directly from Repository
            EmployeeTask actual = taskRepository.Get(expected.TaskId);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test9TaskDelete()
        {
            // Call Delete API
            taskController.Delete(1);
            // Get data directly from Repository
            EmployeeTask actual = taskRepository.Get(1);

            Assert.IsTrue(actual == null);
        }
    }
}
