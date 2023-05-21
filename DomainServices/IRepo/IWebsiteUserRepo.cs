
using Domain.Models;

namespace DomainServices.IRepo
{
    public interface IWebsiteUserRepo
    {
        public void Add(WebsiteUser user);
        public WebsiteUser GetWebsiteUserById(int userId);
        public IEnumerable<WebsiteUser>GetAllWebsiteUsers();
        public WebsiteUser GetWebsiteUserByEmail(string email);
        public void Delete(WebsiteUser user);
        public void SignUpGameNight(int gameNightId, WebsiteUser user);
        public IEnumerable<Food> GetFoodOfUserInGameNight(string userEmail, int gameNightId);
        public void AddFoodOfUserToGameNight(int gameNightId, Food food);
        public void DeleteGameNightSignUp(int gameNightId, string userEmail);
        public IEnumerable<GameNight> GetOrganizedGameNights(string userEmail);
        IEnumerable<GameNight> GetSignedUpGameNights(string userEmail);


    }
}
