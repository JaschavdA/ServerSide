using System.Collections;
using System.Security.Claims;
using Bordspelavond.IdentityObject;
using Domain.Models;
using DomainServices.IRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace Bordspelavond.Controllers
{
    public class BoardGameController : Controller
    {
        private readonly IBoardGameRepo repo;
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebsiteUserRepo websiteUserRepo;

        public BoardGameController(IBoardGameRepo repo, ILogger<HomeController> logger, UserManager<AppUser> _userManager, IWebsiteUserRepo websiteUserRepo)
        {
            this.repo = repo;
            this._logger = logger;
            this._userManager = _userManager;
            this.websiteUserRepo = websiteUserRepo;
        }
        
        [Authorize(policy: "Organizer")]
        public IActionResult Index()
        {
            IEnumerable<BoardGame> games = repo.GetBoardGamesOfUserByEmail(_userManager.GetUserAsync(this.User).Result.Email);
            return View(games);
        }

        public IActionResult GameInfo(int gameId, int? gameNightId)

        {
            if (gameNightId != null)
            {
                ViewBag.GameNightId = gameNightId;
            }
            BoardGame game = repo.GetBoardGameByID(gameId);
            _logger.LogDebug(gameId.ToString());
            @ViewBag.Picture = Convert.ToBase64String(game.Picture);
            return View(game);
        }
        [Authorize(policy: "Organizer")]
        public IActionResult AddGame()
        {
            return View();
        }
        [Authorize(policy: "Organizer")]
        [HttpPost]
        public IActionResult AddGame(BoardGame game, IFormFile pic)
        {
            if (ModelState.IsValid)
            {
                game.User = websiteUserRepo.GetWebsiteUserByEmail(_userManager.GetUserAsync(this.User).Result.Email);
                var memoryStream = new MemoryStream();
                pic.CopyTo(memoryStream);
                game.Picture = memoryStream.ToArray();
                repo.SaveBoardGame(game);
                return RedirectToAction("Index", "Home");
            }
            return View(game);

        }

        
    }
}
