using Microsoft.AspNetCore.Mvc;
using WebovaApkaMM.Models;

namespace WebovaApkaMM.Controllers
{
    public class WebController : Controller
    {
        private readonly MyDbWeb _context;


        public WebController(MyDbWeb context)
        {
            _context = context;
        }
        public IActionResult Index(string sortOrder)
        {
            ViewBag.TaskSortParm = String.IsNullOrEmpty(sortOrder) ? "Task_desc" : "";
            ViewBag.ActivitySortParm = sortOrder == "Activity" ? "Activity_desc" : "Activity";
            ViewBag.StartTimeSortParm = sortOrder == "StartTime" ? "StartTime_desc" : "StartTime";
            ViewBag.StopTimeSortParm = sortOrder == "StopTime" ? "StopTime_desc" : "StopTime";

            var employees = from s in _context.Employees
                           select s;

            switch (sortOrder)
            {
                case "Task_desc":
                    employees = employees.OrderByDescending(s => s.Task );
                    break;
                case "Activity":
                    employees = employees.OrderBy(s => s.Activity);
                    break;
                case "Activity_desc":
                    employees = employees.OrderByDescending(s => s.Activity);
                    break;
                case "StartTime":
                    employees = employees.OrderBy(s => s.StartTime);
                    break;
                case "StartTime_desc":
                    employees = employees.OrderByDescending(s => s.StartTime);
                    break;
                case "StopTime":
                    employees = employees.OrderBy(s => s.StopTime);
                    break;
                case "StopTime_desc":
                    employees = employees.OrderByDescending(s => s.StopTime);
                    break;
                default:
                    employees = employees.OrderBy(s => s.Task);
                    break;
            }
            
            ViewData["users"] = employees.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {

                _context.Add(employee);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Warning));
            }
        }
        public IActionResult Warning()
        {
            return View();
        }
        
        public IActionResult Delete(int? id)
        {
            var delete = _context.Employees.FirstOrDefault(m => m.Id == id);
            if (delete != null)
            {
                _context.Remove(delete);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Index");

        }


    }
}
