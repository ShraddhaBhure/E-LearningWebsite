using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

using System.IO;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using C_Services;
using AspNetCoreHero.ToastNotification.Abstractions;
using C_Models;
using C_Data;

namespace E_LearningMVC.Controllers
{

    public class ArticleController : Controller
    {
        private readonly IArticlesRepository _repository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly INotyfService _notyf;
        private readonly myDbContext _dbContext;
        public ArticleController(IArticlesRepository repository, myDbContext dbContext, INotyfService notyf, IWebHostEnvironment webHostEnvironment)
        {
            _repository = repository;
            _notyf = notyf;
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var articles = await _repository.GetAllArticles();
            return View(articles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Article article, IFormFile coverImage, IFormFile articleFile)
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
                    article.CoverImage = uniqueFileName;
                }

                if (articleFile != null && articleFile.Length > 0)
                {
                    var articlePath = Path.Combine(_webHostEnvironment.WebRootPath, "Article");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + articleFile.FileName;
                    var filePath = Path.Combine(articlePath, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await articleFile.CopyToAsync(fileStream);
                    }
                    article.ArticleFileName = uniqueFileName;
                }

                await _repository.AddArticle(article);
                _notyf.Success("Article Added Successfully");
                return RedirectToAction("Index");
            }

            return View(article);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var article = await _repository.GetArticleById(id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Article article, IFormFile coverImage, IFormFile articleFile)
        {
            if (id != article.ArticleID)
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
                    article.CoverImage = uniqueFileName;
                }

                if (articleFile != null && articleFile.Length > 0)
                {
                    var articlePath = Path.Combine(_webHostEnvironment.WebRootPath, "articles");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + articleFile.FileName;
                    var filePath = Path.Combine(articlePath, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await articleFile.CopyToAsync(fileStream);
                    }
                    article.ArticleFileName = uniqueFileName;
                }

                await _repository.UpdateArticle(article);
                _notyf.Information("Article Edited Successfully");
                return RedirectToAction("Index");
            }

            return View(article);
        }
      
      
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var article = await _repository.GetArticleById(id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await _repository.GetArticleById(id);
            if (article == null)
            {
                return NotFound();
            }

            await _repository.DeleteArticle(id);
            _notyf.Warning("Article Deleted Successfully");
            return RedirectToAction("Index");
        }
        

     

        public async Task<IActionResult> RecentlyAddedArticle()
        {
            var recentlyAddedArticle = await _repository.GetRecentlyAddedArticle();
            return View(recentlyAddedArticle);
        }

        public async Task<IActionResult> Details(int id, Article article, IFormFile coverImage)
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
                article.CoverImage = uniqueFileName;
            }

            Article retrievedArticle = await _repository.GetArticleById(id);

            if (retrievedArticle == null)
            {
                return NotFound();
            }

            return View(retrievedArticle);
        }

        private class FileDownloadResult : Article
        {
            private MemoryStream memoryStream;
            private string contentType;

            public FileDownloadResult(MemoryStream memoryStream, string contentType, string articleFileName)
            {
                this.memoryStream = memoryStream;
                this.contentType = contentType;
                ArticleFileName = articleFileName;
            }
        }
    }

}
