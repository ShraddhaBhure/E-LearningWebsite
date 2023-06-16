using C_Models;
using C_Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
namespace E_LearningMVC.Controllers
{
    public class FeedbackController : Controller
    {
      
            private readonly IFeedbackRepository _feedbackRepository;
            private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly INotyfService _notyf;
        public FeedbackController(IFeedbackRepository feedbackRepository, INotyfService notyf, IWebHostEnvironment webHostEnvironment)
            {
                _feedbackRepository = feedbackRepository;
                _webHostEnvironment = webHostEnvironment;
            _notyf = notyf;
        }



        [HttpGet]
        public async Task<IActionResult> FeedbackIndex()
        {
            var feedbacks = await _feedbackRepository.GetAllAsync();
            return View(feedbacks);
        }


        [HttpGet]
            public async Task<IActionResult> Index()
            {
                var feedbacks = await _feedbackRepository.GetAllAsync();
                return View(feedbacks);
            }

      
        [HttpGet]
            public IActionResult Create()
            {
                return View();
            }

            [HttpPost]
            public async Task<IActionResult> Create(Feedback feedback, IFormFile clientImage)
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

                _notyf.Success("Feedback Added Sucessfully");
                return RedirectToAction("Index");
                }

                return View(feedback);
            }
       
        [HttpGet]
            public async Task<IActionResult> Edit(int id)
            {
                var feedback = await _feedbackRepository.GetByIdAsync(id);
                if (feedback == null)
                {
                    return NotFound();
                }
                return View(feedback);
            }

            [HttpPost]
            public async Task<IActionResult> Edit(int id, Feedback feedback, IFormFile clientImage)
            {
                if (id != feedback.Fid)
                {
                    return NotFound();
                }

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

                    await _feedbackRepository.UpdateAsync(feedback);
                _notyf.Information("Feedback Edited Sucessfully ");
                return RedirectToAction("Index");
                }

                return View(feedback);
            }

            [HttpGet]
            public async Task<IActionResult> Delete(int id)
            {
                var feedback = await _feedbackRepository.GetByIdAsync(id);
                if (feedback == null)
                {
                    return NotFound();
                }
                return View(feedback);
            }

            [HttpPost]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                await _feedbackRepository.DeleteAsync(id);
            _notyf.Warning("Feedback Deleted Sucessfully");
            return RedirectToAction("Index");
            }


       
        public ActionResult ThankYouPage()
        {
            return View();
        }
    }

    }

