using Bordspelavond.IdentityObject;
using Bordspelavond.Models;
using DomainServices.IRepo;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Domain.Models;

namespace Bordspelavond.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IWebsiteUserRepo _websiteUserRepo;

        public AuthController(ILogger<HomeController> logger, UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager, IWebsiteUserRepo websiteUserRepo)
        {
            _logger = logger;
            this._userManager = _userManager;
            this._signInManager = _signInManager;
            this._websiteUserRepo = websiteUserRepo;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            AppUser user = _userManager.FindByEmailAsync(email).Result;
            if (user != null)
            {

                var result = _signInManager.PasswordSignInAsync(user, password, false, false).Result;
                if (result.Succeeded)
                {

                    TempData["UserEmail"] = user.Email;
                    return RedirectToAction("CheckIf18");
                }
               ModelState.AddModelError("", "Invalid username or password");
                return View();
                
            }
            ModelState.AddModelError("", "Invalid username or password");

            return View();

        }

        public IActionResult CheckIf18()
        {
            
            if (!User.HasClaim("EighteenPlus", "EighteenPlus"))
            {
                if (TempData["UserEmail"] != null)
                {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    string email = TempData["UserEmail"].ToString();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                    AppUser user = _userManager.FindByEmailAsync(email).Result;
                    if (user.IsEighteen())
                    {
                        _userManager.GetClaimsAsync(user).Result.Add(new Claim("EighteenPlus", "EighteenPlus"));
                        _userManager.UpdateAsync(user).Wait();
                    }

                }

                return RedirectToAction("Index", "Home");

            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(WebsiteUser webUser, DateTime birthday,
            string password, string repeatpassword, bool isOrganizer)
        {
            if (password.Equals(repeatpassword))
            {
                AppUser user = new AppUser()
                {
                    FirstName = webUser.FirstName,
                    LastName = webUser.LastName,
                    BirthDay = birthday,
                    Email = webUser.Email,
                    UserName = webUser.Email
                };

                if (user.IsSixteen())
                {
                    var result = _userManager.CreateAsync(user, password).Result;
                    if (result.Succeeded)
                    {
                        if (user.IsEighteen())
                        {
                            _userManager.AddClaimAsync(user, new Claim("EighteenPlus", "EighteenPlus")).Wait();
                        }

                        if (isOrganizer)
                        {
                            _userManager.AddClaimAsync(user, new Claim("Organizer", "Organizer")).Wait();
                        }

                        _websiteUserRepo.Add(webUser);

                        var signInResult = _signInManager.PasswordSignInAsync(user, password, false, false).Result;
                        if (signInResult.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                else
                {
                    return RedirectToAction("TooYoung", "Account");
                }


            }

            return RedirectToAction("Register");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
