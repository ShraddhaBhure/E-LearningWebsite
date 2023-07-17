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
using System.Linq;
using Microsoft.Office.Interop.Word;
using iTextSharp.text.pdf;
using iTextSharp.text;

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
                    var allowedExtensions = new[] { ".doc", ".docx", ".pdf" };
                    var fileExtension = Path.GetExtension(articleFile.FileName).ToLower();

                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        ModelState.AddModelError(string.Empty, "Only .doc, .docx, and .pdf files are allowed.");
                        TempData["ErrorMessage"] = "Only .doc, .docx, and .pdf files are allowed.";
                        return View(article);
                    }

                    var articlePath = Path.Combine(_webHostEnvironment.WebRootPath, "Article");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + articleFile.FileName;
                    var filePath = Path.Combine(articlePath, uniqueFileName);

                    // Save the uploaded file
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await articleFile.CopyToAsync(fileStream);
                    }

                    if (fileExtension != ".pdf")
                    {
                        var pdfFilePath = Path.ChangeExtension(filePath, ".pdf");

                        // Convert the non-PDF file to PDF
                        var wordApp = new Microsoft.Office.Interop.Word.Application();
                        var wordDoc = wordApp.Documents.Open(filePath);
                        wordDoc.SaveAs(pdfFilePath, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF);
                        wordDoc.Close();
                        wordApp.Quit();

                        // Delete the original non-PDF file
                        System.IO.File.Delete(filePath);

                        // Update the article's file name to the PDF file
                        article.ArticleFileName = Path.GetFileName(pdfFilePath);
                    }
                    else
                    {
                        // Set the article's file name to the uploaded PDF file
                        article.ArticleFileName = uniqueFileName;
                    }
                }

                await _repository.AddArticle(article);
                TempData["SuccessMessage"] = "Article Added Successfully";
                return RedirectToAction("Index");
            }

            return View(article);
        }


        ////////////[HttpPost]
        ////////////public async Task<IActionResult> Create(Article article, IFormFile coverImage, IFormFile articleFile)
        ////////////{
        ////////////    if (ModelState.IsValid)
        ////////////    {
        ////////////        if (coverImage != null && coverImage.Length > 0)
        ////////////        {
        ////////////            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images");
        ////////////            var uniqueFileName = Guid.NewGuid().ToString() + "_" + coverImage.FileName;
        ////////////            var filePath = Path.Combine(imagePath, uniqueFileName);
        ////////////            using (var fileStream = new FileStream(filePath, FileMode.Create))
        ////////////            {
        ////////////                await coverImage.CopyToAsync(fileStream);
        ////////////            }
        ////////////            article.CoverImage = uniqueFileName;
        ////////////        }

        ////////////        if (articleFile != null && articleFile.Length > 0)
        ////////////        {
        ////////////            var articlePath = Path.Combine(_webHostEnvironment.WebRootPath, "Article");
        ////////////            var uniqueFileName = Guid.NewGuid().ToString() + "_" + articleFile.FileName;
        ////////////            var filePath = Path.Combine(articlePath, uniqueFileName);
        ////////////            using (var fileStream = new FileStream(filePath, FileMode.Create))
        ////////////            {
        ////////////                await articleFile.CopyToAsync(fileStream);
        ////////////            }
        ////////////            article.ArticleFileName = uniqueFileName;
        ////////////        }

        ////////////        await _repository.AddArticle(article);
        ////////////        _notyf.Success("Article Added Successfully");
        ////////////        return RedirectToAction("Index");
        ////////////    }

        ////////////    return View(article);
        ////////////}
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

        public async Task<IActionResult> DownloadArticleFile(int id)
        {
            var article = await _repository.GetArticleById(id);
            if (article == null || string.IsNullOrEmpty(article.ArticleFileName))
            {
                return NotFound();
            }

            var articlePath = Path.Combine(_webHostEnvironment.WebRootPath, "Article", article.ArticleFileName);
            var contentType = "application/pdf"; // Set the appropriate content type based on the file type

            return PhysicalFile(articlePath, contentType, article.ArticleFileName);
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
