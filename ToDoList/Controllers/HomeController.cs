using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext context;
        public HomeController(ApplicationDbContext ctx) => context = ctx;

   

        public IActionResult Index(string id)
        {
            var filters = new Filters(id);

            ViewBag.Filters = filters;

            ViewBag.Categories = context.Categories.ToList();
            ViewBag.Statuses = context.Statuses.ToList();
            ViewBag.DueFilters = ToDoList.Models.Filters.DueFilterValues;

            IQueryable<MyTask> query = context.Task
                .Include(t => t.Category)
                .Include(t => t.Status);

            if(filters.HasCategory)
            {
                query = query.Where(t=>t.CategoryId == id);
            }

            if(filters.HasStatus)
            {
                query = query.Where(t=> t.StatusId == filters.StatusId);
            }
            if(filters.HasDue)
            {
                var today = DateTime.Today;
                if(filters.IsPast)
                {
                    query = query.Where(t=>t.DueDate < today);
                }
                else if(filters.IsFuture) 
                {
                    query.Where(t=>t.DueDate > today);
                }
                else if(filters.IsToday)
                {
                    query = query.Where(t => t.DueDate == today);
                }
            }

            var task = query.OrderBy(t => t.DueDate).ToList();
            return View(task);
         }

        public IActionResult Add()
        {
            ViewBag.Categories = context.Categories.ToList(); ;
            ViewBag.Statues = context.Statuses.ToList();

            var task = new MyTask { StatusId = "open" };
            return View(task);
        }

        [HttpPost]
        public IActionResult Add(MyTask task)
        {
            if(ModelState.IsValid)
            {
                context.Task.Add(task);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Categories = context.Categories.ToList();
                ViewBag.Statues = context.Statuses.ToList(); 
                ViewBag.Task = task;

                return View(task);
            }

        }

        [HttpPost]
        public IActionResult Filters(string[] filter)
        {
            string id = string.Join('-', filter);
            return RedirectToAction("Index",new { ID=id });
        }

        [HttpPost]

        public IActionResult MarkComplete([FromRoute] string id, MyTask selected)
        {
            selected = context.Task.Find(selected.Id);

            if(selected != null)
            {
                selected.StatusId = "closed";
                context.SaveChanges();
            }
            return RedirectToAction("Index", new {ID=id});
        }

        [HttpPost]
        public IActionResult DeleteComplete(string id)
        {
            var toDelete = context.Task.Where(t=>t.StatusId == "closed").ToList();

            foreach(var task in toDelete)
            {
                context.Task.Remove(task);
            }
            context.SaveChanges();
            return RedirectToAction("Index", new { ID=id});
        }


            


    }
}
