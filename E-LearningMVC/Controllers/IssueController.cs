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
using AspNetCoreHero.ToastNotification.Abstractions;

namespace E_LearningMVC.Controllers
{
    public class IssueController : Controller
    {
       
            private readonly IIssueMasterRepository _issueMasterRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly INotyfService _notyf;
        public IssueController(IIssueMasterRepository issueMasterRepository, INotyfService notyf, IWebHostEnvironment webHostEnvironment)
            {
                _issueMasterRepository = issueMasterRepository; 
            _notyf = notyf;
            _webHostEnvironment = webHostEnvironment;
        }

            public async Task<IActionResult> Index()
            {
                var issues = await _issueMasterRepository.GetAllIssues();
                return View(issues);
            }

            public IActionResult Create()
            {
                PopulateDropdownLists();
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(IssueMaster issue, IFormFile frontImageFile, IFormFile pdfFile)
            {
                if (ModelState.IsValid)
                {
                    if (frontImageFile != null)
                    {
                        string imagePath = await SaveFile(frontImageFile, "IssuesImage");
                        issue.Frontimage = imagePath;
                    }

                    if (pdfFile != null)
                    {
                        string pdfPath = await SaveFile(pdfFile, "IssuesFiles");
                        issue.Filename = pdfPath;
                    }

                    issue.IpAddress = GetClientIpAddress();
                    await _issueMasterRepository.CreateIssue(issue);
                    return RedirectToAction("Index");
                }

                PopulateDropdownLists();
                return View(issue);
            }

        //public async Task<IActionResult> Edit(int id)
        //{
        //    var issue = await _issueMasterRepository.GetIssueById(id);
        //    if (issue == null)
        //    {
        //        return NotFound();
        //    }

        //    PopulateDropdownLists();
        //    return View(issue);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, IssueMaster issue, IFormFile frontImageFile, IFormFile pdfFile)
        //{
        //    if (id != issue.IssueID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        if (frontImageFile != null && frontImageFile.Length > 0)
        //        {


        //            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "IssuesImage");
        //            var uniqueFileName = Guid.NewGuid().ToString() + "_" + frontImageFile.FileName;
        //            var filePath = Path.Combine(imagePath, uniqueFileName);
        //            using (var fileStream = new FileStream(filePath, FileMode.Create))
        //            {
        //                await frontImageFile.CopyToAsync(fileStream);
        //            }
        //            issue.Frontimage = uniqueFileName;
        //        }

        //        if (pdfFile != null && pdfFile.Length > 0)
        //        {

        //            var pdfPath = Path.Combine(_webHostEnvironment.WebRootPath, "IssuesFiles");
        //            var uniqueFileName = Guid.NewGuid().ToString() + "_" + pdfFile.FileName;
        //            var filePath = Path.Combine(pdfPath, uniqueFileName);
        //            using (var fileStream = new FileStream(filePath, FileMode.Create))
        //            {
        //                await pdfFile.CopyToAsync(fileStream);
        //            }
        //            issue.Filename = uniqueFileName;
        //        }

        //        issue.IpAddress = GetClientIpAddress();
        //        await _issueMasterRepository.UpdateIssue(issue);
        //        return RedirectToAction("Index");
        //    }

        //    PopulateDropdownLists();
        //    return View(issue);
        //}

        //    public async Task<IActionResult> Delete(int id)
        //    {
        //        var issue = await _issueMasterRepository.GetIssueById(id);
        //        if (issue == null)
        //        {
        //            return NotFound();
        //        }

        //        return View(issue);
        //    }

        //    [HttpPost, ActionName("Delete")]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var issue = await _issueMasterRepository.GetIssueById(id);
        //    if (issue == null)
        //    {
        //        return NotFound();
        //    }
        //    await _issueMasterRepository.DeleteIssue(id);
        //    _notyf.Warning("Issue Deleted Successfully");

        //    return RedirectToAction("Index");
        //    }
        //public async Task<IActionResult> Edit(int id)
        //{
        //    var issue = await _issueMasterRepository.GetIssueById(id);
        //    if (issue == null)
        //    {
        //        return NotFound();
        //    }

        //    PopulateDropdownLists();
        //    return View(issue);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, IssueMaster issue, IFormFile frontImageFile)
        //{
        //    if (id != issue.IssueID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        if (frontImageFile != null && frontImageFile.Length > 0)
        //        {
        //            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "IssuesImage");
        //            var uniqueFileName = Guid.NewGuid().ToString() + "_" + frontImageFile.FileName;
        //            var filePath = Path.Combine(imagePath, uniqueFileName);
        //            using (var fileStream = new FileStream(filePath, FileMode.Create))
        //            {
        //                await frontImageFile.CopyToAsync(fileStream);
        //            }
        //            issue.Frontimage = uniqueFileName;
        //        }

        //        issue.IpAddress = GetClientIpAddress();
        //        await _issueMasterRepository.UpdateIssue(issue);
        //        return RedirectToAction("Index");
        //    }

        //    PopulateDropdownLists();
        //    return View(issue);
        //}

        public async Task<IActionResult> Edit(int id)
        {
            var issue = await _issueMasterRepository.GetIssueById(id);
            if (issue == null)
            {
                return NotFound();
            }

            PopulateDropdownLists();
            return View(issue);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IssueMaster issue, IFormFile frontImageFile, IFormFile pdfFile)
        {
            if (id != issue.IssueID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (frontImageFile != null && frontImageFile.Length > 0)
                {
                    var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "IssuesImage");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + frontImageFile.FileName;
                    var filePath = Path.Combine(imagePath, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await frontImageFile.CopyToAsync(fileStream);
                    }
                    issue.Frontimage = uniqueFileName;
                }

                if (pdfFile != null && pdfFile.Length > 0)
                {
                    var pdfPath = Path.Combine(_webHostEnvironment.WebRootPath, "IssuesFiles");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + pdfFile.FileName;
                    var filePath = Path.Combine(pdfPath, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await pdfFile.CopyToAsync(fileStream);
                    }
                    issue.Filename = uniqueFileName;
                }

                issue.IpAddress = GetClientIpAddress();
                await _issueMasterRepository.UpdateIssue(issue);
                return RedirectToAction("Index");
            }

            PopulateDropdownLists();
            return View(issue);
        }



        public async Task<IActionResult> Delete(int id)
        {
            var issue = await _issueMasterRepository.GetIssueById(id);
            if (issue == null)
            {
                return NotFound();
            }

            return View(issue);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var issue = await _issueMasterRepository.GetIssueById(id);
            if (issue == null)
            {
                return NotFound();
            }

            // Delete the front image file
            if (!string.IsNullOrEmpty(issue.Frontimage))
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "IssuesImage", issue.Frontimage);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            // Delete the front image file
            if (!string.IsNullOrEmpty(issue.Frontimage))
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "IssuesFiles", issue.Filename);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            await _issueMasterRepository.DeleteIssue(id);
            _notyf.Warning("Issue Deleted Successfully");

            return RedirectToAction("Index");
        }

        private void PopulateDropdownLists()
        {
            ViewBag.Display1Types = new List<string> { "vol1", "vol2", "vol3", "vol4", "vol5" };
            ViewBag.Display2Types = new List<string> { "vol1", "vol2", "vol3", "vol4", "vol5" };
        }

        //private void PopulateDropdownLists()
        //    {
        //        ViewBag.Display1Types = new List<string> { "vol1", "vol2", "vol3", "vol4", "vol5" };
        //        ViewBag.Display2Types = new List<string> { "vol1", "vol2", "vol3", "vol4", "vol5" };
        //    }

            private async Task<string> SaveFile(IFormFile file, string folderName)
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName, uniqueFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return Path.Combine("/", folderName, uniqueFileName).Replace("\\", "/");
            }

            private string GetClientIpAddress()
            {
                string ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
                if (ipAddress == "::1") // If running locally
                {
                    ipAddress = "127.0.0.1";
                }
                return ipAddress;
            }

        public async Task<IActionResult> Details(int id, IssueMaster issue, IFormFile coverImage)
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
                issue.Frontimage = uniqueFileName;
            }

            IssueMaster retrievedArticle = await _issueMasterRepository.GetIssueById(id);

            if (retrievedArticle == null)
            {
                return NotFound();
            }

            return View(retrievedArticle);
        }

    }

    }

