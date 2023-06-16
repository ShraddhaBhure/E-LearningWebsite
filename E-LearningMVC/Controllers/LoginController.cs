using C_Models;
using C_Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace E_LearningMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly INotyfService _notyf;
        private readonly ICrudeRepository<Login> _loginRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private const string ReCaptchaSecretKey = "6LeRGW0mAAAAADQYoYZFZ7bDT5BHSWX7A8I6STJu"/*"YOUR_SECRET_KEY"*/;

        public LoginController(ICrudeRepository<Login> loginRepository, INotyfService notyf, IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory)
               {
            _loginRepository = loginRepository;
            _notyf = notyf;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        //[HttpPost]
        //public async Task<IActionResult> Login(Login model, string captchaResponse)
        //{
        //    Validate captcha response
        //    bool isValidCaptcha = await ValidateCaptcha(captchaResponse);
        //    if (!isValidCaptcha)
        //    {
        //        ModelState.AddModelError("captcha", "Invalid captcha.");
        //        _notyf.Error("Invalid captcha.");
        //        return View(model);
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        var user = await _loginRepository.GetAll().SingleOrDefaultAsync(u => u.UserName == model.UserName && u.UserPassword == model.UserPassword);

        //        if (user != null)
        //        {
        //            ViewBag.SuccessMessage = "Login successful.";
        //            TempData["SuccessNotification"] = "Login successful.";
        //            _notyf.Success("Success Notification");

        //            return RedirectToAction("Index", "Login");
        //        }

        //        ModelState.AddModelError("", "Invalid username or password");
        //        _notyf.Error("Invalid username or password");
        //    }

        //    return View(model);
        //}

        //private async Task<bool> ValidateCaptcha(string captchaResponse)
        //{
        //    var httpClient = _httpClientFactory.CreateClient();
        //    var response = await httpClient.PostAsync("https://www.google.com/recaptcha/api/siteverify",
        //        new FormUrlEncodedContent(new Dictionary<string, string>
        //{
        //    { "secret", ReCaptchaSecretKey },
        //    { "response", captchaResponse }
        //}));

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var jsonResponse = await response.Content.ReadAsStringAsync();
        //        var captchaResult = JsonConvert.DeserializeObject<CaptchaResult>(jsonResponse);
        //        return captchaResult.Success;
        //    }

        //    return false;
        //}
        [HttpGet]
        public IActionResult Userlogin()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Userlogin(Login model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _loginRepository.GetAll().SingleOrDefaultAsync(u => u.UserName == model.UserName && u.UserPassword == model.UserPassword);

        //        if (user != null)
        //        {
        //            ViewBag.SuccessMessage = "Login  successfull.";
        //            TempData["SuccessNotification"] = "Login successfull.";
        //            _notyf.Success("Success Notification");

                    
        //            return RedirectToAction("Index", "Login");
        //        }
        //        // ViewBag.SuccessMessage = "Login created successfully.";
        //        ModelState.AddModelError("", "Invalid username or password");
        //        _notyf.Error("Invalid username or password");
        //    }


        //    return View(model);
        //}


        //[HttpPost]
        //public async Task<IActionResult> Login(Login model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _loginRepository.GetAll().SingleOrDefaultAsync(u => u.UserName == model.UserName && u.UserPassword == model.UserPassword);

        //        if (user != null)
        //        {
        //            ViewBag.SuccessMessage = "Login  successfull.";
        //            TempData["SuccessNotification"] = "Login successfull.";
        //            _notyf.Success("Success Notification");

        //            //  return RedirectToAction("Index", "Home");
        //            return RedirectToAction("Index", "Login");
        //        }
        //        // ViewBag.SuccessMessage = "Login created successfully.";
        //        ModelState.AddModelError("", "Invalid username or password");
        //        _notyf.Error("Invalid username or password");
        //    }


        //    return View(model);
        //}


        //[HttpPost]
        //public async Task<IActionResult> Login(Login model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _loginRepository.GetAll().SingleOrDefaultAsync(u => u.UserName == model.UserName && u.UserPassword == model.UserPassword);

        //        if (user != null)
        //        {
        //            ViewBag.SuccessMessage = "Login successful.";
        //            TempData["SuccessNotification"] = "Login successful.";
        //            _notyf.Success("Success Notification");

        //            if (user.UserRole == "Author")
        //            {
        //                return RedirectToAction("Index", "Author");
        //            }
        //            else if (user.UserRole == "Reviewer")
        //            {
        //                return RedirectToAction("Index", "Reviewer");
        //            }
        //            else
        //            {
        //                // Redirect to a default page if the user role is not specified
        //                return RedirectToAction("Index", "Home");
        //            }
        //        }

        //        ModelState.AddModelError("", "Invalid username or password");
        //        _notyf.Error("Invalid username or password");
        //    }

        //    return View(model);
        //}



        [HttpPost]
        public async Task<IActionResult> Userlogin(Login model)
        {
            if (ModelState.IsValid)
            {
                var user = await _loginRepository.GetAll().SingleOrDefaultAsync(u => u.UserName == model.UserName && u.UserPassword == model.UserPassword);

                if (user != null)
                {
                  
               
                 //   _notyf.Success("Success Notification");

                    if (user.UserRole == "Author")
                    {
                        _notyf.Success("Author Login Successfully  ");
                        return RedirectToAction("Index", "Author", new { area = "Author" });
                    }
                    else if (user.UserRole == "Reviewer")
                    {
                        _notyf.Success("Reviewer Login Successfully  ");
                        return RedirectToAction("Index", "Reviewer", new { area = "Reviewer" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                        _notyf.Success("User Login Successfully ");
                    }
                }

             

                _notyf.Error("Invalid username or password");
            }

            return View(model);
        }



        //////////[HttpPost]
        //////////public async Task<IActionResult> Login(Login model)
        //////////{
        //////////    if (ModelState.IsValid)
        //////////    {
        //////////        var user = await _userManager.FindByNameAsync(model.UserName);

        //////////        if (user != null && await _userManager.CheckPasswordAsync(user, model.UserPassword))
        //////////        {
        //////////            await _signInManager.SignInAsync(user, isPersistent: false);

        //////////            Check the user role
        //////////            if (await _userManager.IsInRoleAsync(user, "Admin"))
        //////////            {
        //////////                Redirect to the admin dashboard
        //////////               return RedirectToAction("AdminDashboard");
        //////////                return RedirectToAction("Index", "Login");
        //////////            }
        //////////            else
        //////////            {
        //////////                Redirect to the regular user dashboard
        //////////                return RedirectToAction("Index", "Login");
        //////////                return RedirectToAction("UserDashboard");
        //////////            }
        //////////        }
        //////////        else
        //////////        {
        //////////            ModelState.AddModelError(string.Empty, "Invalid username or password");
        //////////        }
        //////////    }

        //////////    return View(model);
        //////////}


        public IActionResult AdminDashboard()
        {
            // Render the admin dashboard view with the admin navbar
            return View();
        }

        public IActionResult UserDashboard()
        {
            // Render the user dashboard view with the user navbar
            return View();
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
                    //   ViewBag.SuccessMessage = "Login created successfully.";
                    TempData["SuccessNotification"] = "Login created successfully.";
                    _notyf.Success("Login details Added Sucessfully");
                    //  _notificationRepository.ShowSuccessNotification("Login created successfully.");

                    ModelState.Clear(); 
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
                _notyf.Information(" Edited Sucessfully ");
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
            _notyf.Warning(" Deleted Sucessfully");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
         
            return View();
        }
        public async Task<IActionResult> Userlist()
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
            _notyf.Success("Updated Sucessfully");
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
                _notyf.Warning("Invalid username");

                return View(model);
            }

            var newPassword = GenerateRandomPassword();

            await UpdatePassword(model.UserName, newPassword);
            ViewBag.SuccessMessage = "Password updated successfully.";

            _notyf.Success("New Password Generated successfullyy");
            return RedirectToAction("ResetPasswordConfirmation");
        }

        private async Task<bool> CheckUserExists(string username)
        {
            var user = await _loginRepository.GetAll()
                .FirstOrDefaultAsync(u => u.UserName == username);
            return user != null;
            _notyf.Information("This username not existds");
        }

        private async Task UpdatePassword(string username, string newPassword)
        {
            var user = await _loginRepository.GetAll()
                .FirstOrDefaultAsync(u => u.UserName == username);
            if (user != null)
            {
                user.UserPassword = newPassword;
                await _loginRepository.UpdateAsync(user);
                _notyf.Success("Password updated successfully");
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
                _notyf.Warning("Invalid username.");
                return View(model);
            }

            await UpdatePassword(model.UserName, model.UserPassword);
            ViewBag.SuccessMessage = "Login created successfully.";
            _notyf.Success("Login created successfully");
            //   return RedirectToAction("ChangePasswordConfirmation");
            return RedirectToAction("Login");
        }

        public IActionResult ChangePasswordConfirmation()
        {
            return View();
        }

      

    }
}

