using BusinessLogicLayer.Repository.Service;
using DatabaseAccessLayer.Models;
using DatabaseAccessLayer.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ToDoList.Controllers
{
    [Authorize(Roles = SD.Role_Admin)]
    public class StatusController : Controller
    {
        #region CTOR

        private readonly StatusService statusService;

        #endregion

        #region Fields

        public StatusController(StatusService statusService)
        {
            this.statusService = statusService;
        }

        #endregion

        #region GetMyStatus

        public async Task<IActionResult>Index()
        {
            var data = await statusService.GetMyStatus();

            return View(data);
        }

        #endregion

        #region GET:Create

        public async Task<IActionResult> Create()
        {
            var dataa = await statusService.GetStatusData();
            ViewData["StatusId"] = new SelectList(dataa, "StatusId", "StatusId");

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
