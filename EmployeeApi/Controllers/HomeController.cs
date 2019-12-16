using EmployeeApi.Models;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace EmployeeApi.Controllers
{
    public class HomeController : Controller
    {
        private static int editEmpId = 0;

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            EmployeeController empController = new EmployeeController();
            List<Employee> empList = (List<Employee>)empController.Get();
            return View(empList);
        }

        public ActionResult Create(Employee emp)
        {
            if (emp.LastName == null)
                return View();
            else // Press Create button, add the employee
            {
                EmployeeController empController = new EmployeeController();
                empController.PostEmployee(emp);
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(int id, Employee emp)
        {
            // Remember which employee we are editing, we need it later when we do task CRUD
            editEmpId = id; 

            EmployeeController empController = new EmployeeController();
            IHttpActionResult result = empController.Get(id);
            var obj = result as OkNegotiatedContentResult<Employee>;
            Employee rtEmp = (Employee)obj.Content;
            if (emp.LastName == null)
                return View(rtEmp);
            else // Press Save button
            {
                rtEmp.LastName = emp.LastName;
                rtEmp.FirstName = emp.FirstName;
                rtEmp.HiredDate = emp.HiredDate;
                empController.PutEmployee(rtEmp);
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int id)
        {
            EmployeeController empController = new EmployeeController();
            IHttpActionResult eresult = empController.Get(id);
            var empobj = eresult as OkNegotiatedContentResult<Employee>;
            Employee emp = (Employee)empobj.Content;

            // If the employee has any tasks, remove them
            TaskController taskController = new TaskController();
            if (emp.TaskIdList != null && emp.TaskIdList.Count > 0)
            {
                foreach (int taskId in emp.TaskIdList)
                {
                    taskController.Delete(taskId);
                }
            }
            // Remove the employee
            empController.Delete(id);

            return RedirectToAction("Index");
        }

        // Task Actions
        public ActionResult TaskDetail()
        {
            ViewBag.EmpId = editEmpId;  //Get employee ID, so we know whose tasks are to be listed

            EmployeeController empController = new EmployeeController();
            IHttpActionResult result = empController.Get(editEmpId);
            var obj = result as OkNegotiatedContentResult<Employee>;
            Employee rtEmp = (Employee)obj.Content;
            List<EmployeeTask> taskList = new List<EmployeeTask>();
            
            if (rtEmp.TaskIdList != null && rtEmp.TaskIdList.Count > 0)
            {
                TaskController taskController = new TaskController();
                IHttpActionResult taskresult;
                //Add editEmpId's tasks to a task list
                foreach (int taskId in rtEmp.TaskIdList)
                {
                    taskresult = taskController.Get(taskId);
                    var taskobj = taskresult as OkNegotiatedContentResult<EmployeeTask>;
                    taskList.Add((EmployeeTask)taskobj.Content);
                }
            }
            return View(taskList);
        }
        public ActionResult TaskCreate(EmployeeTask task)
        {
            if (task.TaskName == null)
                return View();
            else // Press Create button, add the task
            {
                TaskController taskController = new TaskController();
                taskController.PostTask(task);

                EmployeeController empController = new EmployeeController();
                IHttpActionResult result = empController.Get(editEmpId);
                var obj = result as OkNegotiatedContentResult<Employee>;
                Employee rtEmp = (Employee)obj.Content;
                if (rtEmp.TaskIdList == null)
                    rtEmp.TaskIdList = new List<int>();
                rtEmp.TaskIdList.Add(task.TaskId);
                empController.PutEmployee(rtEmp);

                return RedirectToAction("TaskDetail");
            }
        }

        public ActionResult TaskEdit(int id, EmployeeTask task)
        {
            TaskController taskController = new TaskController();
            IHttpActionResult result = taskController.Get(id);
            var obj = result as OkNegotiatedContentResult<EmployeeTask>;
            EmployeeTask rtTask = (EmployeeTask)obj.Content;
            if (task.TaskName == null)
                return View(rtTask);
            else // Press Save button, update the task
            {
                rtTask.TaskName = task.TaskName;
                rtTask.StartDate = task.StartDate;
                rtTask.DeadLine = task.DeadLine;
                taskController.PutTask(rtTask);
                return RedirectToAction("TaskDetail");
            }
        }

        public ActionResult TaskDelete(int id)
        {
            // Delete task
            TaskController taskController = new TaskController();
            taskController.Delete(id);

            // Delete task from Employee's task list
            EmployeeController empController = new EmployeeController();
            IHttpActionResult result = empController.Get(editEmpId);
            var obj = result as OkNegotiatedContentResult<Employee>;
            Employee emp = (Employee)obj.Content;
            if (emp.TaskIdList != null)
            {
                emp.TaskIdList.Remove(id);
                empController.PutEmployee(emp);
            }

            return RedirectToAction("TaskDetail");
        }
    }
}
