using Microsoft.AspNetCore.Mvc;

namespace E_LearningMVC.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AdminDashboard()
        {
            // Perform admin-specific logic or retrieve data required for the admin dashboard

            return View();
        }
    }
}
