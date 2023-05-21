using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace DomainServices.NewFolder
{
    public class UserLogic
    {
        public static bool IsSignedUpForGameNight(string userEmail, GameNight gameNight)
        {
            bool IsSignedIn = false;
            foreach (var websiteUser in gameNight.Participants)
            {
                if (websiteUser.Email == userEmail)
                {
                    IsSignedIn = true;
                    break;
                }
            }

            return IsSignedIn;
        }

        public static bool UserDoesNotHaveGameNightSignUpOnGameNightDate(WebsiteUser user, GameNight gameNight)
        {
            foreach (var gn in user.GameNight)
            {
                if (gn.DateTime.Date == gameNight.DateTime.Date)
                {
                    return false;
                }   
            }

            return true;
        }

        public static bool DietRequirementsMatchGameNight(WebsiteUser user, GameNight gameNight)
        {
            
            if (user.AlcoholFree)
            {
                if (!gameNight.AlcoholFreeDrinks)
                {
                    return false;
                }
            }
            if (user.NutAllergy)
            {
                if (!gameNight.NutFreeSnacks)
                {
                    return false;
                }
            }

            if (user.IsVegetarian)
            {
                if (!gameNight.VegetarianSnacks)
                {
                    return false;
                }
            }

            if (user.LactoseIntolerant)
            {
                if (!gameNight.LactoseFreeSnacks)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
