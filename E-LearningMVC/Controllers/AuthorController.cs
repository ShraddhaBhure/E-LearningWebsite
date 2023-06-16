using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using C_Models;
using C_Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;


using System.IO;
using AspNetCoreHero.ToastNotification.Abstractions;

using Microsoft.EntityFrameworkCore;


namespace E_LearningMVC.Controllers
{
    public class AuthorController : Controller
    {
        private readonly ICrudeRepository<Article> _articleRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly INotyfService _notyf;
        public AuthorController(ICrudeRepository<Article> articleRepository, INotyfService notyf, IWebHostEnvironment webHostEnvironment)
        {
            _articleRepository = articleRepository;
            _webHostEnvironment = webHostEnvironment;
            _notyf = notyf;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SubbmitBook()
        {
            return View();
        }
        // Index action - Get all articles
        public async Task<IActionResult> ArticlsList()
        {
            var articles = await _articleRepository.GetAllAsync();
            return View(articles);
        }

        // Create action - GET
        public IActionResult Create()
        {
            return View();
        }

        // Create action - POST
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Articles article, IFormFile coverImageFile)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (coverImageFile != null && coverImageFile.Length > 0)
        //        {
        //            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Article");
        //            var uniqueFileName = Guid.NewGuid().ToString() + "_" + coverImageFile.FileName;
        //            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //            using (var fileStream = new FileStream(filePath, FileMode.Create))
        //            {
        //                await coverImageFile.CopyToAsync(fileStream);
        //            }
        //            article.CoverImage = uniqueFileName;
        //        }

        //        await _articleRepository.InsertAsync(article);
        //        _notyf.Success("Article Added Sucessfully");
        //        return RedirectToAction("Index");
        //    }

        //    return View(article);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Article article, IFormFile coverImage, IFormFile articleFile)
        {
            if (ModelState.IsValid)
            {
                // Save the uploaded cover image file
                if (coverImage != null && coverImage.Length > 0)
                {
                    //using (var memoryStream = new MemoryStream())
                    //{
                    //    await coverImage.CopyToAsync(memoryStream);
                    //    article.CoverImage = memoryStream.ToArray();
                    //}
                }

                // Save the uploaded article file
                if (articleFile != null && articleFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await articleFile.CopyToAsync(memoryStream);
                        article.ArticleFileName = articleFile.FileName;
                    }
                }

                await _articleRepository.InsertAsync(article);
                return RedirectToAction("Index");
            }

            return View(article);
        }
    
    // Edit action - GET
    public async Task<IActionResult> Edit(Article arti)
        {
            await _articleRepository.UpdateAsync(arti);

            return View(arti);
        }

        // Edit action - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Article article, IFormFile coverImageFile)
        {
            if (id != article.ArticleID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (coverImageFile != null && coverImageFile.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Article");
                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + coverImageFile.FileName;
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await coverImageFile.CopyToAsync(fileStream);
                        }
                        article.CoverImage = uniqueFileName;
                    }

                    await _articleRepository.UpdateAsync(article);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.ArticleID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction("Index");
            }

            return View(article);
        }

        // Delete action - GET
        public async Task<IActionResult> Delete(Guid id)
        {
            var article = await _articleRepository.GetByIdAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // Delete action - POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _articleRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        private bool ArticleExists(int id)
        {
            // Check if the article exists in the repository
            // Implement this method based on your repository implementation
            // For example:
            // return _articleRepository.GetByIdAsync(id) != null;
            throw new NotImplementedException();
        }
    }
}
