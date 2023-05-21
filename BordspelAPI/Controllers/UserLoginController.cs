using Bordspelavond.IdentityObject;
using Domain.Models;
using DomainServices.IRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BordspelAPI.Controllers
{
    [Route("REST/[controller]")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {

        private readonly IWebsiteUserRepo websiteUserRepo;
        private readonly UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;

        public UserLoginController(IWebsiteUserRepo websiteUserRepo, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            this.websiteUserRepo = websiteUserRepo;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        [HttpPost]
        public ActionResult<WebsiteUser> Post(LoginDTO userlogin)
        {
            var user = userManager.FindByEmailAsync(userlogin.email).Result;
            if (user != null)
            {
                var result = signInManager.PasswordSignInAsync(user, userlogin.password, false, false).Result;
                if (result.Succeeded)
                {
                    return Ok(websiteUserRepo.GetWebsiteUserByEmail(user.Email));
                }
            }

            return BadRequest("Password or Email are incorrect");

        }
    }
}


