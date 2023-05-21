using Domain.Models;
using DomainServices.IRepo;

namespace BordspelAPI
{
    public class Query
    {

        public IEnumerable<GameNight> GetGameNight([Service] IGameNightRepo _gameNightRepo)
        {
            var test = _gameNightRepo.GetAllGameNights();
            return test;
        }

        public IEnumerable<GameNight> GetAllGameNightsInFuture([Service] IGameNightRepo _gameNightRepo)
        {
            var test = _gameNightRepo.GetAllGameNightsInFuture();
            return test;
        }
    }
}
