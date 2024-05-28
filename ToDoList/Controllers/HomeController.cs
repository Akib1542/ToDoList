using Microsoft.AspNetCore.Mvc;
using DatabaseAccessLayer.Data;

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
    }
}
