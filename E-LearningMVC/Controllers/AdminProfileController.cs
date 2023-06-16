using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using C_Models;
using C_Services;
using Microsoft.EntityFrameworkCore;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Security.Claims;
using C_Data;

namespace E_LearningMVC.Controllers
{
    public class AdminProfileController : Controller
    {
        private readonly INotyfService _notyf;
        private readonly ICrudeRepository<Login> _loginRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly myDbContext _context;
        public AdminProfileController(ICrudeRepository<Login> loginRepository, UserManager<IdentityUser> userManager,INotyfService notyf, IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory, myDbContext context)
        {
            _userManager = userManager;
            _loginRepository = loginRepository;
            _notyf = notyf;
            _httpClientFactory = httpClientFactory;
            _context = context;
        }
    

        public async Task<IActionResult> Details()
        {
            // Get the current logged-in user's username from the ClaimsPrincipal
            var userName = User.FindFirstValue(ClaimTypes.Name);

            // Retrieve the user from the database based on the username
            var user = await _context.Login.FirstOrDefaultAsync(u => u.UserName == userName);

            if (user == null)
            {
                // User not found, handle accordingly (e.g., redirect to an error page)
                return NotFound();
            }

            return View(user);
        }
    

        public IActionResult Index()
        {
            return View();
        }
        //[Authorize]
        //public async Task<IActionResult> UserDetails()
        //{
        ////    IdentityUser currentUser = await _userManager.GetUserAsync(User);

        ////    if (currentUser == null)
        ////    {
        ////        return NotFound();
        ////    }

        //   return View(currentUser);
        //}
    }
}
