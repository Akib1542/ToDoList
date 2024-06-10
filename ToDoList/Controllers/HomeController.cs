using Microsoft.AspNetCore.Mvc;
using DatabaseAccessLayer.Data;
using DatabaseAccessLayer.Models;
using System.Diagnostics;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        #region CTOR
        private readonly ApplicationDbContext context;
        public HomeController()
        {
            
        }
        #endregion

        #region Fields
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion 
    }
}
