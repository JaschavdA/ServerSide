using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DomainServices.Logic
{
    public class GameNightLogic
    {
        public static bool GameNightIsVegetarian(GameNight gameNight)
        {
            foreach (var f in gameNight.Food)
            {
                if (f.IsVegetarian)
                {
                    return true;
                }
            }

            return false;
        }
        public static bool GameNightIsLactoseFree(GameNight gameNight)
        {
            foreach (var f in gameNight.Food)
            {
                if (f.IsLactoseFree)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool GameNightIsNutFree(GameNight gameNight)
        {
            foreach (var f in gameNight.Food)
            {
                if (f.IsNutFree)
                {
                    return true;
                }
            }

            return false;
        }
        public static bool GameNightIsAlcoholFree(GameNight gameNight)
        {
            foreach (var f in gameNight.Food)
            {
                if (f.IsAlcoholFree)
                {
                    return true;
                }
            }

            return false;
        }

        public static Address CreateAddress(string street, int houseNumber, string city, string postalCode)
        {
            return new Address()
            {
                Street = street,
                HouseNumber = houseNumber,
                City = city,
                PostalCode = postalCode
            };
        }

        public static bool IsStill18Plus(IEnumerable<BoardGame> gamesInGameNight)
        {
            foreach (var game in gamesInGameNight)
            {
                if (game.Is18Plus)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool CanBeEditted(GameNight gameNight)
        {
            return gameNight.Participants.Count < 1;
        }
    }
}
