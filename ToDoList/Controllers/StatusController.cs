using BusinessLogicLayer.Repository.Service;
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
        private readonly StatusService statusService;
        #endregion

        #region Fields
        public StatusController(ApplicationDbContext context, StatusService statusservice)
        {
            _context = context;
            statusService = statusservice;
        }
        #endregion

        #region GetMyStatus
        public async Task<IActionResult>Index()
        {
            var data = await statusService?.GetMyStatus();

            return View(data);
        }
        #endregion

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
            var data = await statusService.AddTask(statu);

            return RedirectToAction("Index");
        }
        #endregion
    }
}
