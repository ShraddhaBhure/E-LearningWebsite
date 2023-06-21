using C_Data;
using C_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Hosting;

namespace E_LearningMVC.Controllers
{
    public class ArticleUploadController : Controller
    {
        private readonly myDbContext _dbContext;
        private readonly INotyfService _notyf; private readonly IWebHostEnvironment _webHostEnvironment;

        public ArticleUploadController(myDbContext dbContext, INotyfService notyf, IWebHostEnvironment webHostEnvironment)
        {
            _notyf = notyf;
            _webHostEnvironment = webHostEnvironment;
            _dbContext = dbContext;
        }
        public IActionResult Create()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult Create(ArticleUpload articleUpload, IFormFile file)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Save the uploaded file
        //        if (file != null && file.Length > 0)
        //        {

        //            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "ArticalImges");
        //            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;

        //            var filePath = Path.Combine(imagePath, uniqueFileName);
        //            using (var stream = new FileStream(filePath, FileMode.Create))
        //            {
        //                file.CopyTo(stream);
        //            }




        //            articleUpload.UploadArticleFile = uniqueFileName;
        //        }

        //        _dbContext.ArticleUpload.Add(articleUpload);
        //        _dbContext.SaveChanges();
        //        _notyf.Success("Articles Added Sucessfully");
        //    }

        //    return View(articleUpload);
        //}

        [HttpPost]
        public IActionResult Create(ArticleUpload articleUpload, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                // Save the uploaded file
                if (file != null && file.Length > 0)
                {
                    var allowedExtensions = new[] { ".pdf", ".doc" };
                    var fileExtension = Path.GetExtension(file.FileName).ToLower();

                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        ModelState.AddModelError(string.Empty, "Only PDF and DOC files are allowed.");
                        _notyf.Success("Only PDF and DOC files are allowed.");
                        return View(articleUpload);
                    }

                    var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "ArticalImges");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;

                    var filePath = Path.Combine(imagePath, uniqueFileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    articleUpload.UploadArticleFile = uniqueFileName;
                }

                _dbContext.ArticleUpload.Add(articleUpload);
                _dbContext.SaveChanges();
                _notyf.Success("Articles Added Successfully");
            }

            return View(articleUpload);
        }

        public IActionResult Index()
        {
            return View();
        }
      
    }
}
