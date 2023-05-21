using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices.IRepo
{
    public interface IGameNightRepo
    {
        IEnumerable<GameNight> GetAllGameNights();
        void SaveGameNight(GameNight gameNight);
        void DeleteGameNight(GameNight gameNight);
        void EditGameNight(GameNight gameNight);
        GameNight GetGameNightById(int id);
        GameNight GetMostRecentGameNightOfUserByEmail(string email);
        IEnumerable<BoardGame> GetBoardGamesNotInGameNight(GameNight gameNight);
        void DeleteGameFromGameNight(int gameId, int gameNightId);

        IEnumerable<GameNight> GetAllGameNightsInFuture();
        IEnumerable<GameNight> GetAllGameNightsInFutureWhereNotOrganizerOrSignedUp(WebsiteUser user);
        public IEnumerable<GameNight> GetAllGameNightsInFutureWhereNotOrganizerOrSignedUpAndGameNightIsNot18Plus(
            WebsiteUser user);

        public void AddGameToGameNight(int gameNightId, int gameId);

    }
}