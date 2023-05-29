using C_Models;
using C_Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace E_LearningMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly ICrudeRepository<Login> _loginRepository;

        public LoginController(ICrudeRepository<Login> loginRepository)
        {
            _loginRepository = loginRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login model)
        {
            if (ModelState.IsValid)
            {
                var user = await _loginRepository.GetAll().SingleOrDefaultAsync(u => u.UserName == model.UserName && u.UserPassword == model.UserPassword);

                if (user != null)
                {
                    ViewBag.SuccessMessage = "Login  successfull.";

                  //  return RedirectToAction("Index", "Home");
                    return RedirectToAction("Index", "Login");

                }
                // ViewBag.SuccessMessage = "Login created successfully.";
                ModelState.AddModelError("", "Invalid username or password");
            }

            return View(model);
        }
    
    

        public IActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Login login)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(login.UserName) || string.IsNullOrWhiteSpace(login.UserPassword))
                {
                    ModelState.AddModelError(string.Empty, "Please enter both username and password.");
                }
                else
                {
                    await _loginRepository.InsertAsync(login);
                    ViewBag.SuccessMessage = "Login created successfully.";
                    ModelState.Clear(); // Clear the model state to reset the form
                }
            }

            return View(login);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var login = await _loginRepository.GetByIdAsync(id);
            if (login == null)
            {
                return NotFound();
            }

            return View(login);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Login login)
        {
            if (ModelState.IsValid)
            {
                await _loginRepository.UpdateAsync(login);
                return RedirectToAction("Index");
            }

            return View(login);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var login = await _loginRepository.GetByIdAsync(id);
            if (login == null)
            {
                return NotFound();
            }

            return View(login);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _loginRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            var loginData = await _loginRepository.GetAllAsync();
            return View(loginData);
        }


        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(Login model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userExists = await CheckUserExists(model.UserName);

            if (!userExists)
            {
                ModelState.AddModelError(string.Empty, "Invalid username.");
                return View(model);
            }

            var newPassword = GenerateRandomPassword();

            await UpdatePassword(model.UserName, newPassword);

            return RedirectToAction("ResetPasswordConfirmation", new { newPassword });
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(Login model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userExists = await CheckUserExists(model.UserName);

            if (!userExists)
            {
                ModelState.AddModelError(string.Empty, "Invalid username.");
                return View(model);
            }

            var newPassword = GenerateRandomPassword();

            await UpdatePassword(model.UserName, newPassword);
            ViewBag.SuccessMessage = "Password updated successfully.";

            return RedirectToAction("ResetPasswordConfirmation");
        }

        private async Task<bool> CheckUserExists(string username)
        {
            var user = await _loginRepository.GetAll()
                .FirstOrDefaultAsync(u => u.UserName == username);
            return user != null;
        }

        private async Task UpdatePassword(string username, string newPassword)
        {
            var user = await _loginRepository.GetAll()
                .FirstOrDefaultAsync(u => u.UserName == username);
            if (user != null)
            {
                user.UserPassword = newPassword;
                await _loginRepository.UpdateAsync(user);
            }
        }

        private string GenerateRandomPassword()
        {
            // Implementation for generating a random password
            // You can use a library like "Security.Cryptography" or generate a simple random string
            // Example: Generate a random password with 10 characters
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            var random = new Random();
            var password = new string(Enumerable.Repeat(validChars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return password;
        }

        public IActionResult ResetPasswordConfirmation(string newPassword)
        {
            ViewBag.NewPassword = newPassword;
            return View();
        }

        [HttpGet]
        public IActionResult ChangePasswordAgain()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePasswordAgain(Login model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userExists = await CheckUserExists(model.UserName);

            if (!userExists)
            {
                ModelState.AddModelError(string.Empty, "Invalid username.");
                return View(model);
            }

            await UpdatePassword(model.UserName, model.UserPassword);
            ViewBag.SuccessMessage = "Login created successfully.";

            //   return RedirectToAction("ChangePasswordConfirmation");
            return RedirectToAction("Login");
        }

        public IActionResult ChangePasswordConfirmation()
        {
            return View();
        }

    }
}

