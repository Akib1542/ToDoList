using BusinessLogicLayer.Repository.Interfaces;
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

        private readonly IStatusService statusService;

        #endregion

        #region Fields

        public StatusController(IStatusService statusService)
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
            var data = await statusService.GetStatusData();
            ViewData["StatusId"] = new SelectList(data, "StatusId", "StatusId");
            if(data==null)
            {
                this.ViewBag.isError = "Status already Exist";
            }
            else
            {
                this.ViewBag.isError = "Ok";
            }
            return View();
        }

        #endregion

        #region POST:Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Status status)
        {
            var data = await statusService.AddTask(status);
            if (data == null)
            {
                this.ViewBag.isError = "Status already Exist";
                return View();
            }
            else
            {
                this.ViewBag.isError = "Ok";
            }

            return RedirectToAction("Index");
        }

        #endregion
    }
}
