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
    public class BooksController : Controller
    {
        private readonly IBooksLibraryRepository _repository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly INotyfService _notyf;


    
        public BooksController(IBooksLibraryRepository repository, INotyfService notyf, IWebHostEnvironment webHostEnvironment)
        {
            _repository = repository; 
            _notyf = notyf;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _repository.GetAllBooks();
            return View(books);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BooksLibrary book, IFormFile coverImage)
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
                    book.CoverImage = uniqueFileName;
                }

                await _repository.AddBook(book);
                _notyf.Success("Books Added Sucessfully");
                return RedirectToAction("Index");
            }

            return View(book);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var book = await _repository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, BooksLibrary book, IFormFile coverImage)
        {
            if (id != book.BookId)
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
                    book.CoverImage = uniqueFileName;
                }

                await _repository.UpdateBook(book);
                _notyf.Information("Book Edited Sucessfully ");
                return RedirectToAction("Index");
            }

            return View(book);
        }


        [HttpGet]
public async Task<IActionResult> Delete(int id)
{
    var book = await _repository.GetBookById(id);
    if (book == null)
    {
        return NotFound();
    }

    return View(book);
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> DeleteConfirmed(int id)
{
    var book = await _repository.GetBookById(id);
    if (book == null)
    {
        return NotFound();
    }

    await _repository.DeleteBook(id);
            _notyf.Warning("Book Deleted Sucessfully");
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                // If the search term is empty, redirect to the index or show an error message
                _notyf.Information("Book Not Found ");
                return RedirectToAction("Index");
            }
            _notyf.Information("Book Found Sucessfully ");
            var results = await _repository.SearchBooks(searchTerm);
            return View(results);
        }

        public async Task<IActionResult> RecentlyAddedBook()
        {
            var recentlyAddedBook = await _repository.GetRecentlyAddedBook();
            return View(recentlyAddedBook);
        }
        
        public async Task<IActionResult> Details(int id, BooksLibrary bookl, IFormFile coverImage)
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
            BooksLibrary book = await _repository.GetBookById(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

    }


}
