using C_Data;
using C_Models;
using C_Services;
using E_LearningMVC.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace E_LearningMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeProjectsRepository _homeprorepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IOnlineClassRepository _onlineClasrepository;
        private readonly myDbContext _dbContext;
        private readonly IBooksLibraryRepository _bookrepository;
        public HomeController(IBooksLibraryRepository brepository, myDbContext dbContext,IHomeProjectsRepository hoprorepository, IOnlineClassRepository onlineClrepository, IWebHostEnvironment webHostEnvironment)
        {
            _onlineClasrepository= onlineClrepository;
               _homeprorepository = hoprorepository;
               _bookrepository = brepository;
            _webHostEnvironment = webHostEnvironment;
            _dbContext = dbContext;
        }

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        public async Task<IActionResult> HomeProjects()
        {
            var projects = await _homeprorepository.GetAllProjects();
            return View(projects);
        }
        public async Task<IActionResult> Onlineclass()
        {
            var classes = await _onlineClasrepository.GetAllClasses();
            return View(classes);
        }

        public async Task<IActionResult> BooksLists()
        {
            var books = await _bookrepository.GetAllBooks();
            return View(books);
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        //public IActionResult Contact()
        //{
        //    return View();
        //}

        [HttpPost]
        public IActionResult Contact(Contactus contact)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Contactus.Add(contact);
                _dbContext.SaveChanges();

                TempData["SuccessMessage"] = "Message sent successfully!";
            }
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new C_Models.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
