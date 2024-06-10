using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DatabaseAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using DatabaseAccessLayer.Utility;
using BusinessLogicLayer.Repository.Service;

namespace ToDoList.Controllers
{
    [Authorize(Roles =SD.Role_TaskUSer)]
    public class MyTasksController : Controller
    {
        #region CTOR
        private readonly MyTaskService mytask;
        private readonly StatusService statusService;
        #endregion

        #region Fields
        public MyTasksController( MyTaskService myTask, StatusService statusservice)
        {
            mytask = myTask;
            statusService = statusservice;
        }
        #endregion

        #region GET: MyTasks

        public async Task<IActionResult> Index(bool isActiveFilter,int pg=1, string search = "")
        {
            var myTasks = await mytask.GetMyTask();
            var data = await mytask.GetCatBySearch(search, isActiveFilter);
            const int pageSize = 3;
            if (pg < 1)
            {
                pg = 1;
            }
            int resCount = data.Count();
            var pager = new Pager(resCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            data = data.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            this.ViewBag.Filter = isActiveFilter;

            return View(data);
        }

        #endregion

        #region MyTasks Details
        public async Task<IActionResult> Details(int id=0)
        {
            var data = await mytask.GetDetails(id);

            return View(data);
        }

        #endregion

        #region GET:Create
        public async Task<IActionResult> Create()
        {
            var StatusData = await statusService.GetStatusData();
            ViewData["StatusId"] = new SelectList(StatusData, "StatusId", "StatusId");

            return View();
        }
        #endregion

        #region POST:Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( MyTask myTask)
        {
            var data = await mytask.AddTask(myTask);

            return RedirectToAction("Index");

        }
        #endregion

        #region GET: Edit
        public async Task<IActionResult> Edit(int id=0)
        {
             var MyTaskData = await statusService.GetStatusData();
           
            if (id == null || MyTaskData == null)
            {
                return NotFound();
            }
            var myTask = await mytask.GetMyEditingTask(id);
            if (myTask == null)
            {
                return NotFound();
            }
            var StatusData = await statusService.GetStatusData();
            ViewData["StatusId"] = new SelectList(StatusData, "StatusId", "StatusId", myTask.StatusId);

            return View(myTask);
        }
        #endregion

        #region POST:Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MyTask myTask)
        {
            if (id != myTask.Id)
            {
                return NotFound();
            }
            var data = await mytask.UpdateTask(myTask);

            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region GET:Delete
        public async Task<IActionResult> Delete(int? id)
        {
            var MyTaskData = await statusService.GetStatusData();
            if (id == null || MyTaskData == null)
            {
                return NotFound();
            }

            var myTask = await mytask.GetMyDeletingTask(id);
            if (myTask == null)
            {
                return NotFound();
            }

            return View(myTask);
        }
        #endregion

        #region POST:Delete
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
        #endregion

    }
}
