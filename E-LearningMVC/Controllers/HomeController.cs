using C_Data;
using C_Models;
using C_Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace E_LearningMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IHomeProjectsRepository _homeprorepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IOnlineClassRepository _onlineClasrepository;
        private readonly myDbContext _dbContext;
        private readonly IBooksLibraryRepository _bookrepository;
        public HomeController(IFeedbackRepository feedbackRepository, IBooksLibraryRepository brepository, myDbContext dbContext,IHomeProjectsRepository hoprorepository, IOnlineClassRepository onlineClrepository, IWebHostEnvironment webHostEnvironment)
        {
            _feedbackRepository = feedbackRepository;
            _onlineClasrepository = onlineClrepository;
               _homeprorepository = hoprorepository;
               _bookrepository = brepository;
            _webHostEnvironment = webHostEnvironment;
            _dbContext = dbContext;
        }
//        _notifyService.Success("This is a Success Notification");
//            _notifyService.Error("This is an Error Notification");
//            _notifyService.Warning("This is a Warning Notification");
//            _notifyService.Information("This is an Information Notification");
//            _notifyService.Success("This toast will be dismissed in 10 seconds.",10);_notifyService.Custom("Custom Notification - closes in 5 seconds.", 5, "whitesmoke");
//_notifyService.Custom("Custom Notification - closes in 5 seconds.", 10, "#135224");
            
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
        [HttpGet]
        public IActionResult FeedCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FeedCreate(Feedback feedback, IFormFile clientImage)
        {
            if (ModelState.IsValid)
            {
                if (clientImage != null && clientImage.Length > 0)
                {
                    var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + clientImage.FileName;
                    var filePath = Path.Combine(imagePath, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await clientImage.CopyToAsync(fileStream);
                    }
                    feedback.ClientImage = uniqueFileName;
                }

                await _feedbackRepository.InsertAsync(feedback);
                return RedirectToAction("Index");
            }

            return View(feedback);
        }
        [HttpGet]
        public async Task<IActionResult> FeedbackIndex()
        {
            var feedbacks = await _feedbackRepository.GetAllAsync();
            return View(feedbacks);
        }


        public async Task<IActionResult> BooksLists()
        {
            var books = await _bookrepository.GetAllBooks();
            return View(books);
        }
        public async Task<IActionResult> DetailsBook(int id, BooksLibrary bookl, IFormFile coverImage)
        {
            if (coverImage != null && coverImage.Length > 0)
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + coverImage.FileName;
                var filePath = Path.Combine(imagePath, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await coverImage.CopyToAsync(fileStream);
                }
                bookl.CoverImage = uniqueFileName;
            }
            BooksLibrary book = await _bookrepository.GetBookById(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
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
