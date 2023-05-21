using Bordspelavond.Data;
using Domain.Models;
using DomainServices.IRepo;
using DomainServices.Logic;
using Microsoft.EntityFrameworkCore;

namespace Bordspelavond.Infrastructure.Repo
{

    public class EFWebsiteUserRepo : IWebsiteUserRepo
    {
        private readonly DomainContext context;
        private readonly IGameNightRepo _gameNightRepo;

        public EFWebsiteUserRepo(IGameNightRepo _gameNightRepo, DomainContext context)
        {
            this._gameNightRepo = _gameNightRepo;
            this.context = context;
        }  
        
        public void Add(WebsiteUser user)
        {
            context.WebsiteUser.Add(user);
            context.SaveChanges();
        }

        public WebsiteUser GetWebsiteUserById(int userId)
        {
            return context.WebsiteUser.Include(u => u.GameNight).Where(u => u.Id == userId).FirstOrDefault();
        }
        public IEnumerable<WebsiteUser> GetAllWebsiteUsers()
        {
            return context.WebsiteUser;
        }

        public WebsiteUser GetWebsiteUserByEmail(string email)
        {
            var user = context.WebsiteUser.Include(u => u.GameNight).Where(u => u.Email.Equals(email)).FirstOrDefault();
            if (user != null)
            {
                return user;
            }

            throw new Exception("User with this email was not found");
        }

        public void Delete(WebsiteUser user)
        {
            context.WebsiteUser.Remove(user);
            context.SaveChangesAsync();
        }


        public void SignUpGameNight(int gameNightId, WebsiteUser user)
        {
            
            var foods = context.Food.Where(f => f.User.Id == user.Id && f.GameNight.Id == gameNightId);
            if (foods.Any())
            {
                foreach (var food in foods)
                {
                    food.IsAddedToGameNight = true;
                }
                var gameNight = context.GameNight.Include(gn => gn.Participants).Where(gn => gn.Id == gameNightId).First();
                gameNight.Participants.Add(user);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Inschrijven is enkel mogelijk als je minimaal een snack meeneemt");
            }
            
        }

        public IEnumerable<Food> GetFoodOfUserInGameNight(string userEmail, int gameNightId)
        {
            return context.Food.Where(u => u.User.Email == userEmail && u.GameNight.Id == gameNightId);
        }

        public void AddFoodOfUserToGameNight(int gameNightId, Food food)
        {
            var gameNight = _gameNightRepo.GetGameNightById(gameNightId);
            gameNight.Food.Add(food);
            _gameNightRepo.EditGameNight(gameNight);
        }

        public void DeleteGameNightSignUp(int gameNightId, string userEmail)
        {
            WebsiteUser websiteUser = context.WebsiteUser.Include(w => w.GameNight).ThenInclude(g => g.Participants)
                .Where(w => w.Email == userEmail).First();


            var foods = context.Food.Where(f => f.GameNight.Id == gameNightId && f.User.Email == userEmail);
            foreach (var food in foods)
            {
                context.Food.Remove(food);
            }

            foreach (var gn in websiteUser.GameNight)
            {
                if (gn.Id == gameNightId)
                {
                    gn.Participants.Remove(websiteUser);
                    break;
                }
            }

            context.SaveChanges();
            var gameNight = _gameNightRepo.GetGameNightById(gameNightId);
            _gameNightRepo.EditGameNight(gameNight);

        }

        public IEnumerable<GameNight> GetOrganizedGameNights(string userEmail)
        {
            var test = context.GameNight.Include(gn => gn.Address).Include(gn => gn.Participants)
                .Where(gn => gn.DateTime > DateTime.Now && gn.Organizer.Email == userEmail).OrderBy(gn => gn.DateTime);
            return test;
        }

        public IEnumerable<GameNight> GetSignedUpGameNights(string userEmail)
        {
            return context.GameNight.Include(gn => gn.Address).Include(gn => gn.Participants)
                .Where(gn => gn.Participants.Contains(GetWebsiteUserByEmail(userEmail)) && gn.DateTime > DateTime.Now);

        }
    }
}
