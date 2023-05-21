using Bordspelavond.IdentityObject;
using DomainServices.IRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bordspelavond.Controllers
{
    public class UserController : Controller
    {

        private readonly IGameNightRepo gameNightRepo;
        private readonly IBoardGameRepo boardGameRepo;
        private readonly IWebsiteUserRepo websiteUserRepo;
        private readonly UserManager<AppUser> userManager;

        public UserController(IGameNightRepo gameNightRepo, IBoardGameRepo boardGameRepo,
            IWebsiteUserRepo websiteUserRepo, UserManager<AppUser> userManager)
        {
            this.gameNightRepo = gameNightRepo;
            this.boardGameRepo = boardGameRepo;
            this.websiteUserRepo = websiteUserRepo;
            this.userManager = userManager;
        }


        [Authorize]
        public IActionResult Index()
        {
            string email = userManager.GetUserAsync(this.User).Result.Email;


            if (User.HasClaim("Organizer", "Organizer"))
            {
                ViewBag.Organisations =
                    websiteUserRepo.GetOrganizedGameNights(email);
            }

            ViewBag.User = websiteUserRepo.GetWebsiteUserByEmail(email);
            var signedUpGameNights = websiteUserRepo.GetSignedUpGameNights(email); 
            return View(signedUpGameNights);
        }



        [Authorize(policy: "Organizer")]
        public IActionResult GameNights()
        {
            var organizedGameNights = websiteUserRepo.GetOrganizedGameNights(userManager.GetUserAsync(this.User).Result.Email);
            return View(organizedGameNights);
        }

    }
}
