using Bordspelavond.Data;
using Bordspelavond.IdentityObject;
using Domain.Models;
using DomainServices.IRepo;
using DomainServices.Logic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Bordspelavond.Infrastructure.Repo
{
    public class EFGameNightRepo : IGameNightRepo
    {
        //private readonly DomainContext context = new DomainContext();
        private readonly DomainContext context;

        public EFGameNightRepo(DomainContext context)
        {
            this.context = context;
        }

        public void DeleteGameNight(GameNight gameNight)
        {
            context.GameNight.Remove(gameNight);
            context.SaveChanges();
        }

        public void EditGameNight(GameNight gameNight)
        {
            var edit = GetGameNightById(gameNight.Id);
            context.Update(edit);

            var address = GetAddress(gameNight);
            if (address != null)
            {
                edit.Address = address;
            }
            else
            {
                edit.Address = gameNight.Address;
            }

            edit.MaxNumberOfPlayers = gameNight.MaxNumberOfPlayers;
            edit.DateTime = gameNight.DateTime;
            edit.Is18Plus = GameNightLogic.IsStill18Plus(edit.Games);
            if (gameNight.Food != null)
            {
                edit.AlcoholFreeDrinks = GameNightLogic.GameNightIsAlcoholFree(gameNight);
                edit.LactoseFreeSnacks = GameNightLogic.GameNightIsLactoseFree(gameNight);
                edit.NutFreeSnacks = GameNightLogic.GameNightIsNutFree(gameNight);
                edit.VegetarianSnacks = GameNightLogic.GameNightIsVegetarian(gameNight);
            }
            context.SaveChanges();
        }

        public IEnumerable<GameNight> GetAllGameNights()
        {
            return context.GameNight.Include(gn => gn.Organizer).Include(gn => gn.Participants).Include(gn => gn.Games).Include(gn => gn.Address).Include(gn => gn.Food).AsEnumerable();
        }


        public GameNight GetGameNightById(int id)
        {
            var gameNight = context.GameNight.Include(o => o.Organizer).Include(g => g.Games).Include(a => a.Address).Include(p => p.Participants).Include(f => f.Food).Where(g => g.Id == id).FirstOrDefault();
            return gameNight;
        }

        public GameNight GetMostRecentGameNightOfUserByEmail(string email)
        {
            var gameNights = context.GameNight.Include(g => g.Games).Include(o => o.Organizer ).Where(g => g.Organizer.Email.Equals(email)).OrderByDescending(g => g.Id)
                .FirstOrDefault();

            if (gameNights != null)
            {
                return gameNights;
            }

            throw new Exception("No game night was found");
        }

        public void SaveGameNight(GameNight gameNight)
        {
            var user = context.WebsiteUser.Find(gameNight.Organizer.Id);
            var address = GetAddress(gameNight);
            if (user != null)
            {
                gameNight.Organizer = user;
            }

            if (address != null)
            {
                gameNight.Address = address;
            }

            context.GameNight.Add(gameNight);
            context.SaveChanges();
            context.Entry(gameNight).State = EntityState.Detached;
        }

        public IEnumerable<BoardGame> GetBoardGamesNotInGameNight(GameNight gameNight)
        {
            return context.BoardGame.Where(g =>
                g.User.Id == gameNight.Organizer.Id && !g.GameNights.Contains(gameNight));
            
        }

        public void DeleteGameFromGameNight(int gameId, int gameNightId)
        {
            var gameNight = GetGameNightById(gameNightId);
            var gameToDelete = context.BoardGame.Find(gameId);
            var gameList = gameNight.Games.ToList();
            for (int i = 0; i < gameList.Count; i++)
            {
                if (gameList[i].Id == gameId)
                {
                    gameList.RemoveAt(i);
                    break;
                }
            }
            gameNight.Is18Plus = GameNightLogic.IsStill18Plus(gameList);
            gameNight.Games = gameList;
            EditGameNight(gameNight);
        }
       
        public IEnumerable<GameNight> GetAllGameNightsInFuture()
        {
            return context.GameNight.Include(gn => gn.Address).Include(gn => gn.Participants)
                .Include(gn => gn.Games).Include(gn => gn.Food).Where(gn =>
                gn.DateTime > DateTime.Now && gn.Participants.Count < gn.MaxNumberOfPlayers);
        }
        public IEnumerable<GameNight> GetAllGameNightsInFutureWhereNotOrganizerOrSignedUp(WebsiteUser user)
        {
            return context.GameNight.Include(gn => gn.Address).Include(gn => gn.Participants).Where(gn =>
                gn.DateTime > DateTime.Now && gn.Organizer != user && !gn.Participants.Contains(user));
        }

        public IEnumerable<GameNight> GetAllGameNightsInFutureWhereNotOrganizerOrSignedUpAndGameNightIsNot18Plus(WebsiteUser user)
        {
            return context.GameNight.Include(gn => gn.Address).Include(gn => gn.Participants).Where(gn =>
                gn.DateTime > DateTime.Now && gn.Organizer != user && !gn.Participants.Contains(user) && !gn.Is18Plus);
        }

        public void AddGameToGameNight(int gameNightId, int gameId)
        {
            var gameNight = GetGameNightById(gameNightId);
            if (gameNight.Games == null)
            {
                gameNight.Games = new List<BoardGame>();
            }
            var game = context.BoardGame.Find(gameId);
            if (game != null)
            {
                if (game.Is18Plus)
                {
                    gameNight.Is18Plus = true;
                }
                gameNight.Games.Add(game);
                EditGameNight(gameNight);
            }
            

        }


        private Address? GetAddress(GameNight gameNight)
        {
           return context.Address.Where(a =>
                    a.City.Equals(gameNight.Address.City) && a.HouseNumber == gameNight.Address.HouseNumber &&
                    a.PostalCode.Equals(gameNight.Address.PostalCode) && a.Street.Equals(gameNight.Address.Street))
                .FirstOrDefault();
        }
    }
}
