using BusinessLogicLayer.Repository.Interface;
using DatabaseAccessLayer.Data;
using DatabaseAccessLayer.Models;
using DatabaseAccessLayer.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Controllers
{
    [Authorize(Roles = SD.Role_Admin)]
    public class StatusController : Controller
    {
        #region CTOR
        private readonly ApplicationDbContext _context;
        private readonly IStatus statuses;
        #endregion

        #region Fields
        public StatusController(ApplicationDbContext context, IStatus statuses)
        {
            _context = context;
            this.statuses = statuses;
        }
        #endregion

        public async Task<IActionResult>Index()
        {
            var data = await statuses.GetMyStatus();
            return View(data);
        }


        #region GET:Create
        public IActionResult Create()
        {
            ViewData["StatusId"] = new SelectList(_context.Statuses, "StatusId", "StatusId");
            return View();
        }
        #endregion

        #region POST:Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Status statu)
        {

            var data = await statuses.AddTask(statu);
            return RedirectToAction("Index");

        }
        #endregion*/
    }
}
