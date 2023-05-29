using C_Models;
using C_Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace E_LearningMVC.Controllers
{
    public class OnlineClassController : Controller
    {
        private readonly IOnlineClassRepository _repository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public OnlineClassController(IOnlineClassRepository repository, IWebHostEnvironment webHostEnvironment)
        {
            _repository = repository;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var classes = await _repository.GetAllClasses();
            return View(classes);
        }

        public IActionResult Create()
        {
            return View();
        
       }

        [HttpPost]
        public async Task<IActionResult> Create(OnlineClass onlineClass, IFormFile coverImage)
        {
            if (ModelState.IsValid)
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
                    onlineClass.CoverImage = uniqueFileName;
                }

                await _repository.AddClass(onlineClass);
                return RedirectToAction("Index");
            }

            return View(onlineClass);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var onlineClass = await _repository.GetClassById(id);
            if (onlineClass == null)
            {
                return NotFound();
            }

            return View(onlineClass);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, OnlineClass onlineClass, IFormFile coverImage)
        {
            if (id != onlineClass.ClassId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
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
                    onlineClass.CoverImage = uniqueFileName;
                }

                await _repository.UpdateClass(onlineClass);
                return RedirectToAction("Index");
            }

            return View(onlineClass);
        }

     


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var onlineClass = await _repository.GetClassById(id);
            if (onlineClass == null)
            {
                return NotFound();
            }

            return View(onlineClass);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var onlineClass = await _repository.GetClassById(id);
            if (onlineClass == null)
            {
                return NotFound();
            }

            await _repository.DeleteClass(id);
            return RedirectToAction("Index");
        }
    }
}
