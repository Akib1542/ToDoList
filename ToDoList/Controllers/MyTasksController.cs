using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DatabaseAccessLayer.Data;
using DatabaseAccessLayer.Models;
using BusinessLogicLayer.Repository.Interface;

namespace ToDoList.Controllers
{
    public class MyTasksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMyTask mytask;

        public MyTasksController(ApplicationDbContext context, IMyTask mytask)
        {
            _context = context;
            this.mytask = mytask;
        }
    

        // GET: MyTasks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Task.Include(m => m.Status);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MyTasks/Details/5
        public async Task<IActionResult> Details(int id=0)
        {
            var data = await mytask.GetDetails(id);
            return View(data);
        }

        // GET: MyTasks/Create
        public IActionResult Create()
        {
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusId");
            return View();
        }

        // POST: MyTasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,DueDate,CategoryId,Category,StatusId")] MyTask myTask)
        {
            if (ModelState.IsValid)
            {
                var data = await mytask.AddTask(myTask);
                return RedirectToAction("Index");
            }
          // ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusId", myTask.StatusId);
            return View(myTask);
        }

        // GET: MyTasks/Edit/5
        public async Task<IActionResult> Edit(int id=0)
        {
            if (id == null || _context.Task == null)
            {
                return NotFound();
            }

            var myTask = await _context.Task.FindAsync(id);
            if (myTask == null)
            {
                return NotFound();
            }
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusId", myTask.StatusId);
            return View(myTask);
        }

        // POST: MyTasks/Edit/5
   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,DueDate,CategoryId,Category,StatusId")] MyTask myTask)
        {
            if (id != myTask.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var data = await mytask.UpdateTask(myTask);
                return RedirectToAction(nameof(Index));
            }
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusId", myTask.StatusId);
            return View(myTask);
        }

        // GET: MyTasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Task == null)
            {
                return NotFound();
            }

            var myTask = await _context.Task
                .Include(m => m.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (myTask == null)
            {
                return NotFound();
            }

            return View(myTask);
        }

        // POST: MyTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                else
                {
                    bool status = await mytask.DeleteRecord(id);
                    if (status)
                    {
                        TempData["userSuccess"] = "Record successfully deleted";
                    }
                    else
                    {
                        TempData["userError"] = "Record not delete";
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return RedirectToAction("Index");
        }

      
    }
}
