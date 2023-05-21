using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainServices.NewFolder;
using Bordspelavond.Data;
using Bordspelavond.Infrastructure.Repo;
using Microsoft.EntityFrameworkCore;

namespace UnitTests
{
    public class WebsiteUserTests
    {
        [Fact]
        public void IsSignedUpForgameNightReturnsTrueIfUserIsSignedUpForGameNight()
        {

            //arrange
            var testUser = new WebsiteUser()
            {
                Id = 1,
                Email = "test@test.nl",
            };

            var testUser2 = new WebsiteUser()
            {
                Id = 2,
                Email = "test2@test.nl",
            };

            var userList = new List<WebsiteUser>();
            userList.Add(testUser);
            userList.Add(testUser2);

            var gameNight = new GameNight()
            {
                Participants = userList
            };
            //act
            var result = UserLogic.IsSignedUpForGameNight(testUser.Email, gameNight);
            //assert
            Assert.True(result);
        }

        [Fact]
        public void IsSignedUpForgameNightReturnsFalseIfUserIsNotSignedUpForGameNight()
        {

            //arrange
            var testUser = new WebsiteUser()
            {
                Id = 1,
                Email = "test@test.nl",
            };

            var testUser2 = new WebsiteUser()
            {
                Id = 2,
                Email = "test2@test.nl",
            };

            var userList = new List<WebsiteUser>();
            userList.Add(testUser2);

            var gameNight = new GameNight()
            {
                Participants = userList
            };
            //act
            var result = UserLogic.IsSignedUpForGameNight(testUser.Email, gameNight);
            //assert
            Assert.False(result);
        }

        [Fact]
        public void
            UserDoesNotHaveGameNightSignUpOnGameNightDateReturnsTrueIfUserDoesNotHaveGameNightSignUpOnGameNightDate()
        {
            //Arrange
            var gameNight = new GameNight()
            {
                DateTime = new DateTime(2022, 1, 1)
            };
            var gameNight2 = new GameNight()
            {
                DateTime = new DateTime(2022, 1, 2)
            };
            var gameNightList = new List<GameNight>();
            gameNightList.Add(gameNight);

            var testUser = new WebsiteUser()
            {
                GameNight = gameNightList
            };
            //act
            var result = UserLogic.UserDoesNotHaveGameNightSignUpOnGameNightDate(testUser, gameNight2);
            //assert
            Assert.True(result);
        }

        [Fact]
        public void
            UserDoesNotHaveGameNightSignUpOnGameNightDateReturnsFalseIfUserDoesHaveGameNightSignUpOnGameNightDate()
        {
            //Arrange
            var gameNight = new GameNight()
            {
                DateTime = new DateTime(2022, 1, 1)
            };
            var gameNight2 = new GameNight()
            {
                DateTime = new DateTime(2022, 1, 2)
            };
            var gameNightList = new List<GameNight>();
            gameNightList.Add(gameNight);
            gameNightList.Add(gameNight2);

            var testUser = new WebsiteUser()
            {
                GameNight = gameNightList
            };
            //act
            var result = UserLogic.UserDoesNotHaveGameNightSignUpOnGameNightDate(testUser, gameNight2);
            //assert
            Assert.False(result);
        }

        [Fact]
        public void DietRequirementsMatchGameNightReturnsTrueIfUserHasNoDietRequirement()
        {

            //arrange
            var testUser = new WebsiteUser()
            {
                AlcoholFree = false,
                NutAllergy = false,
                IsVegetarian = false,
                LactoseIntolerant = false
            };

            var gameNight = new GameNight()
            {
                LactoseFreeSnacks = false,
                VegetarianSnacks = false,
                NutFreeSnacks = false,
                AlcoholFreeDrinks = false
            };
            //act
            var result = UserLogic.DietRequirementsMatchGameNight(testUser, gameNight);

            Assert.True(result);

        }

        [Fact]
        public void DietRequirementsMatchGameNightReturnsTrueIfUserIsVegetarianAndGameNightHasVegetarianSnacks()
        {

            //arrange
            var testUser = new WebsiteUser()
            {
                AlcoholFree = false,
                NutAllergy = false,
                IsVegetarian = true,
                LactoseIntolerant = false
            };

            var gameNight = new GameNight()
            {
                LactoseFreeSnacks = false,
                VegetarianSnacks = true,
                NutFreeSnacks = false,
                AlcoholFreeDrinks = false
            };
            //act
            var result = UserLogic.DietRequirementsMatchGameNight(testUser, gameNight);

            Assert.True(result);

        }

        [Fact]
        public void DietRequirementsMatchGameNightReturnsTrueIfUserIsLactoseIntolerantAndGameNightHasLactoseFreeSnacks()
        {

            //arrange
            var testUser = new WebsiteUser()
            {
                AlcoholFree = false,
                NutAllergy = false,
                IsVegetarian = false,
                LactoseIntolerant = true
            };

            var gameNight = new GameNight()
            {
                LactoseFreeSnacks = true,
                VegetarianSnacks = false,
                NutFreeSnacks = false,
                AlcoholFreeDrinks = false
            };
            //act
            var result = UserLogic.DietRequirementsMatchGameNight(testUser, gameNight);

            Assert.True(result);

        }

        [Fact]
        public void DietRequirementsMatchGameNightReturnsTrueIfUserHasNutAllergyAndGameNightHasNutFreeSnacks()
        {

            //arrange
            var testUser = new WebsiteUser()
            {
                AlcoholFree = false,
                NutAllergy = true,
                IsVegetarian = false,
                LactoseIntolerant = false
            };

            var gameNight = new GameNight()
            {
                LactoseFreeSnacks = false,
                VegetarianSnacks = true,
                NutFreeSnacks = true,
                AlcoholFreeDrinks = false
            };
            //act
            var result = UserLogic.DietRequirementsMatchGameNight(testUser, gameNight);

            Assert.True(result);

        }

        [Fact]
        public void DietRequirementsMatchGameNightReturnsTrueIfUserIsAlcoholFreeAndGameNightHasAlcoholFreeDrinks()
        {

            //arrange
            var testUser = new WebsiteUser()
            {
                AlcoholFree = true,
                NutAllergy = false,
                IsVegetarian = false,
                LactoseIntolerant = false
            };

            var gameNight = new GameNight()
            {
                LactoseFreeSnacks = false,
                VegetarianSnacks = false,
                NutFreeSnacks = false,
                AlcoholFreeDrinks = true
            };
            //act
            var result = UserLogic.DietRequirementsMatchGameNight(testUser, gameNight);

            Assert.True(result);

        }

        [Fact]
        public void DietRequirementsMatchGameNightReturnsTrueIfUserHasAllRequirementsAndGameNightMeetsAllRequirements()
        {

            //arrange
            var testUser = new WebsiteUser()
            {
                AlcoholFree = true,
                NutAllergy = true,
                IsVegetarian = true,
                LactoseIntolerant = true
            };

            var gameNight = new GameNight()
            {
                LactoseFreeSnacks = true,
                VegetarianSnacks = true,
                NutFreeSnacks = true,
                AlcoholFreeDrinks = true
            };
            //act
            var result = UserLogic.DietRequirementsMatchGameNight(testUser, gameNight);

            Assert.True(result);

        }

        [Fact]
        public void DietRequirementsMatchGameNightReturnsFalseIfGameNightDoesNotMatchRequirements()
        {

            //arrange
            var testUser = new WebsiteUser()
            {
                AlcoholFree = false,
                NutAllergy = false,
                IsVegetarian = false,
                LactoseIntolerant = true
            };

            var gameNight = new GameNight()
            {
                LactoseFreeSnacks = false,
                VegetarianSnacks = true,
                NutFreeSnacks = true,
                AlcoholFreeDrinks = true
            };
            //act
            var result = UserLogic.DietRequirementsMatchGameNight(testUser, gameNight);

            Assert.False(result);

        }

        [Fact]
        public void AddAddsUser()
        {

            //arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainContext>().UseInMemoryDatabase("InMemDBWebsiteUser").Options;
            var domainContext = new DomainContext(dbContextOptions);
            var testUser = new WebsiteUser()
            {
                Id = 1,
                FirstName = "test",
                LastName = "test",
                Email = "test@test.nl",
                IsVegetarian = true,
                NutAllergy = true,
                LactoseIntolerant = true,
                AlcoholFree = true,
                GameNight = new List<GameNight>()
            };
            var helper = new EFGameNightRepo(domainContext);
            var sut = new EFWebsiteUserRepo(helper, domainContext);

            //act
            sut.Add(testUser);
            var result = domainContext.WebsiteUser.ToList();
            //assert
            Assert.True(result.Count == 1);
            domainContext.Database.EnsureDeleted();
        }

        [Fact]
        public void GetWebsiteUserByIdRetrievesUser1IfIdIs1()
        {

            //arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainContext>().UseInMemoryDatabase("InMemDBWebsiteUser").Options;
            var domainContext = new DomainContext(dbContextOptions);
            var helper = new EFGameNightRepo(domainContext);
            var sut = new EFWebsiteUserRepo(helper, domainContext);

            var testUser = new WebsiteUser()
            {
                Id = 1,
                FirstName = "test",
                LastName = "test",
                Email = "test@test.nl",
                IsVegetarian = true,
                NutAllergy = true,
                LactoseIntolerant = true,
                AlcoholFree = true,
                GameNight = new List<GameNight>()
            };
            var testUser2 = new WebsiteUser()
            {
                Id = 2,
                FirstName = "test",
                LastName = "test",
                Email = "test2@test.nl",
                IsVegetarian = true,
                NutAllergy = true,
                LactoseIntolerant = true,
                AlcoholFree = true,
                GameNight = new List<GameNight>()
            };
            domainContext.WebsiteUser.Add(testUser);
            domainContext.WebsiteUser.Add(testUser2);
            domainContext.SaveChanges();
            //act
            var result = sut.GetWebsiteUserById(1).Id;

            Assert.Equal(1, result);
            domainContext.Database.EnsureDeleted();
        }

        [Fact]
        public void GetWebsiteUserByIdRetrievesU2IfIdIs2()
        {

            //arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainContext>().UseInMemoryDatabase("InMemDBWebsiteUser").Options;
            var domainContext = new DomainContext(dbContextOptions);
            var helper = new EFGameNightRepo(domainContext);
            var sut = new EFWebsiteUserRepo(helper, domainContext);

            var testUser = new WebsiteUser()
            {
                Id = 1,
                FirstName = "test",
                LastName = "test",
                Email = "test@test.nl",
                IsVegetarian = true,
                NutAllergy = true,
                LactoseIntolerant = true,
                AlcoholFree = true,
                GameNight = new List<GameNight>()
            };
            var testUser2 = new WebsiteUser()
            {
                Id = 2,
                FirstName = "test",
                LastName = "test",
                Email = "test2@test.nl",
                IsVegetarian = true,
                NutAllergy = true,
                LactoseIntolerant = true,
                AlcoholFree = true,
                GameNight = new List<GameNight>()
            };
            domainContext.WebsiteUser.Add(testUser);
            domainContext.WebsiteUser.Add(testUser2);
            domainContext.SaveChanges();
            //act
            var result = sut.GetWebsiteUserById(2).Id;

            Assert.Equal(2, result);
            domainContext.Database.EnsureDeleted();
        }

        [Fact]
        public void GetAllWebsiteUsersRetrievesAllWebsiteUsers()
        {
            //arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainContext>().UseInMemoryDatabase("InMemDBWebsiteUser").Options;
            var domainContext = new DomainContext(dbContextOptions);
            var helper = new EFGameNightRepo(domainContext);
            var sut = new EFWebsiteUserRepo(helper, domainContext);

            var testUser = new WebsiteUser()
            {
                Id = 1,
                FirstName = "test",
                LastName = "test",
                Email = "test@test.nl",
                IsVegetarian = true,
                NutAllergy = true,
                LactoseIntolerant = true,
                AlcoholFree = true,
                GameNight = new List<GameNight>()
            };
            var testUser2 = new WebsiteUser()
            {
                Id = 2,
                FirstName = "test",
                LastName = "test",
                Email = "test2@test.nl",
                IsVegetarian = true,
                NutAllergy = true,
                LactoseIntolerant = true,
                AlcoholFree = true,
                GameNight = new List<GameNight>()
            };
            domainContext.WebsiteUser.Add(testUser);
            domainContext.WebsiteUser.Add(testUser2);
            domainContext.SaveChanges();
            //act
            var result = sut.GetAllWebsiteUsers();

            Assert.True(result.Count() == 2);
            domainContext.Database.EnsureDeleted();
        }

        [Fact]
        public void GetWebsiteUserByEmailReturnsUserTestIfEmailIsTest()
        {
            //arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainContext>().UseInMemoryDatabase("InMemDBWebsiteUser").Options;
            var domainContext = new DomainContext(dbContextOptions);
            var helper = new EFGameNightRepo(domainContext);
            var sut = new EFWebsiteUserRepo(helper, domainContext);

            var testUser = new WebsiteUser()
            {
                Id = 1,
                FirstName = "test",
                LastName = "test",
                Email = "test@test.nl",
                IsVegetarian = true,
                NutAllergy = true,
                LactoseIntolerant = true,
                AlcoholFree = true,
                GameNight = new List<GameNight>()
            };
            var testUser2 = new WebsiteUser()
            {
                Id = 2,
                FirstName = "test",
                LastName = "test",
                Email = "henk@test.nl",
                IsVegetarian = true,
                NutAllergy = true,
                LactoseIntolerant = true,
                AlcoholFree = true,
                GameNight = new List<GameNight>()
            };
            domainContext.WebsiteUser.Add(testUser);
            domainContext.WebsiteUser.Add(testUser2);
            domainContext.SaveChanges();
            //act
            var result = sut.GetWebsiteUserByEmail(testUser.Email);
            //assert
            Assert.Equal(testUser.Email, result.Email);
            domainContext.Database.EnsureDeleted();
        }

        [Fact]
        public void GetWebsiteUserByEmailReturnsUserHenkIfEmailIsHenk()
        {
            //arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainContext>().UseInMemoryDatabase("InMemDBWebsiteUser").Options;
            var domainContext = new DomainContext(dbContextOptions);
            var helper = new EFGameNightRepo(domainContext);
            var sut = new EFWebsiteUserRepo(helper, domainContext);

            var testUser = new WebsiteUser()
            {
                Id = 1,
                FirstName = "test",
                LastName = "test",
                Email = "test@test.nl",
                IsVegetarian = true,
                NutAllergy = true,
                LactoseIntolerant = true,
                AlcoholFree = true,
                GameNight = new List<GameNight>()
            };
            var testUser2 = new WebsiteUser()
            {
                Id = 2,
                FirstName = "henk",
                LastName = "test",
                Email = "henk@test.nl",
                IsVegetarian = true,
                NutAllergy = true,
                LactoseIntolerant = true,
                AlcoholFree = true,
                GameNight = new List<GameNight>()
            };
            domainContext.WebsiteUser.Add(testUser);
            domainContext.WebsiteUser.Add(testUser2);
            domainContext.SaveChanges();
            //act
            var result = sut.GetWebsiteUserByEmail(testUser2.Email);
            //assert
            Assert.Equal(testUser2.Email, result.Email);
            domainContext.Database.EnsureDeleted();
        }

        [Fact]
        public void DeleteDeletesUserIfUserExists()
        {
            //arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainContext>().UseInMemoryDatabase("InMemDBWebsiteUser").Options;
            var domainContext = new DomainContext(dbContextOptions);
            var helper = new EFGameNightRepo(domainContext);
            var sut = new EFWebsiteUserRepo(helper, domainContext);

            var testUser = new WebsiteUser()
            {
                Id = 1,
                FirstName = "test",
                LastName = "test",
                Email = "test@test.nl",
                IsVegetarian = true,
                NutAllergy = true,
                LactoseIntolerant = true,
                AlcoholFree = true,
                GameNight = new List<GameNight>()
            };
            var testUser2 = new WebsiteUser()
            {
                Id = 2,
                FirstName = "henk",
                LastName = "test",
                Email = "henk@test.nl",
                IsVegetarian = true,
                NutAllergy = true,
                LactoseIntolerant = true,
                AlcoholFree = true,
                GameNight = new List<GameNight>()
            };
            domainContext.WebsiteUser.Add(testUser);
            domainContext.WebsiteUser.Add(testUser2);
            domainContext.SaveChanges();
            //act
            sut.Delete(testUser);
            var result = domainContext.WebsiteUser.ToList();
            //assert
            Assert.True(result.Count == 1);
            Assert.True(result[0].Id == 2);
            domainContext.Database.EnsureDeleted();

        }

        [Fact]
        public void DeleteUserDoesNotDeleteUserIfUserDoesNotExist()
        {
            //arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainContext>().UseInMemoryDatabase("InMemDBWebsiteUser").Options;
            var domainContext = new DomainContext(dbContextOptions);
            var helper = new EFGameNightRepo(domainContext);
            var sut = new EFWebsiteUserRepo(helper, domainContext);

            var testUser = new WebsiteUser()
            {
                Id = 1,
                FirstName = "test",
                LastName = "test",
                Email = "test@test.nl",
                IsVegetarian = true,
                NutAllergy = true,
                LactoseIntolerant = true,
                AlcoholFree = true,
                GameNight = new List<GameNight>()
            };
            var testUser2 = new WebsiteUser()
            {
                Id = 2,
                FirstName = "henk",
                LastName = "test",
                Email = "henk@test.nl",
                IsVegetarian = true,
                NutAllergy = true,
                LactoseIntolerant = true,
                AlcoholFree = true,
                GameNight = new List<GameNight>()
            };

            var testUser3 = new WebsiteUser()
            {
                Id = 3,
                FirstName = "henk",
                LastName = "test",
                Email = "henk2@test.nl",
                IsVegetarian = true,
                NutAllergy = true,
                LactoseIntolerant = true,
                AlcoholFree = true,
                GameNight = new List<GameNight>()
            };
            domainContext.WebsiteUser.Add(testUser);
            domainContext.WebsiteUser.Add(testUser2);
            domainContext.SaveChanges();
            //act
            sut.Delete(testUser3);
            var result = domainContext.WebsiteUser.ToList();
            //assert
            Assert.True(result.Count == 2);
            domainContext.Database.EnsureDeleted();
        }

        [Fact]
        public void SignUpGameNightSignsUpIfUserBringsFood()
        {
            //arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainContext>().UseInMemoryDatabase("InMemDBWebsiteUser").Options;
            var domainContext = new DomainContext(dbContextOptions);
            var helper = new EFGameNightRepo(domainContext);
            var sut = new EFWebsiteUserRepo(helper, domainContext);

            var testUser = new WebsiteUser()
            {
                Id = 1,
                FirstName = "test",
                LastName = "test",
                Email = "test@test.nl",
                IsVegetarian = true,
                NutAllergy = true,
                LactoseIntolerant = true,
                AlcoholFree = true,
                GameNight = new List<GameNight>()
            };

            var gameNight = new GameNight()
            {
                Organizer = testUser,
                Address = new Address()
                {
                    Street = "test",
                    City = "test",
                    HouseNumber = 1,
                    PostalCode = "test"
                },
                MaxNumberOfPlayers = 1,
                VegetarianSnacks = true,
                LactoseFreeSnacks = true,
                NutFreeSnacks = true,
                AlcoholFreeDrinks = true,
                Participants = new List<WebsiteUser>(),
                Games = new List<BoardGame>(),
                Food = new List<Food>()
            };

            var food = new Food()
            {
                Id = 1,
                Name = "test",
                User = testUser,
                IsAddedToGameNight = false,
                IsAlcoholFree = false,
                IsLactoseFree = false,
                IsNutFree = false,
                IsVegetarian = false,
                GameNight = gameNight
            };

            domainContext.WebsiteUser.Add(testUser);
            domainContext.GameNight.Add(gameNight);
            domainContext.Food.Add(food);
            domainContext.SaveChanges();
            //act
            sut.SignUpGameNight(gameNight.Id, testUser);
            var result = domainContext.GameNight.Include(p => p.Participants).Where(g => g.Id == gameNight.Id).First()
                .Participants;
            Assert.True(result.Count ==1);
            domainContext.Database.EnsureDeleted();
        }

        [Fact]
        public void SignUpGameNightThrowsExceptionIfUserDoesNotBringFood()
        {
            //arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainContext>().UseInMemoryDatabase("InMemDBWebsiteUser").Options;
            var domainContext = new DomainContext(dbContextOptions);
            var helper = new EFGameNightRepo(domainContext);
            var sut = new EFWebsiteUserRepo(helper, domainContext);

            var testUser = new WebsiteUser()
            {
                Id = 1,
                FirstName = "test",
                LastName = "test",
                Email = "test@test.nl",
                IsVegetarian = true,
                NutAllergy = true,
                LactoseIntolerant = true,
                AlcoholFree = true,
                GameNight = new List<GameNight>()
            };

            var gameNight = new GameNight()
            {
                Organizer = testUser,
                Address = new Address()
                {
                    Street = "test",
                    City = "test",
                    HouseNumber = 1,
                    PostalCode = "test"
                },
                MaxNumberOfPlayers = 1,
                VegetarianSnacks = true,
                LactoseFreeSnacks = true,
                NutFreeSnacks = true,
                AlcoholFreeDrinks = true,
                Participants = new List<WebsiteUser>(),
                Games = new List<BoardGame>(),
                Food = new List<Food>()
            };


            domainContext.WebsiteUser.Add(testUser);
            domainContext.GameNight.Add(gameNight);
            domainContext.SaveChanges();
            //act&assert
            Assert.Throws<Exception>(() => sut.SignUpGameNight(gameNight.Id, testUser));
            domainContext.Database.EnsureDeleted();
        }

        [Fact]
        public void GetFoodOfUserInGameNightReturnsOneFoodIfUserHasAddedOneFood()
        {
            //arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainContext>().UseInMemoryDatabase("InMemDBWebsiteUser").Options;
            var domainContext = new DomainContext(dbContextOptions);
            var helper = new EFGameNightRepo(domainContext);
            var sut = new EFWebsiteUserRepo(helper, domainContext);

            var testUser = new WebsiteUser()
            {
                Id = 1,
                FirstName = "test",
                LastName = "test",
                Email = "test@test.nl",
                IsVegetarian = true,
                NutAllergy = true,
                LactoseIntolerant = true,
                AlcoholFree = true,
                GameNight = new List<GameNight>()
            };

            var gameNight = new GameNight()
            {
                Organizer = testUser,
                Address = new Address()
                {
                    Street = "test",
                    City = "test",
                    HouseNumber = 1,
                    PostalCode = "test"
                },
                MaxNumberOfPlayers = 1,
                VegetarianSnacks = true,
                LactoseFreeSnacks = true,
                NutFreeSnacks = true,
                AlcoholFreeDrinks = true,
                Participants = new List<WebsiteUser>(),
                Games = new List<BoardGame>(),
                Food = new List<Food>()
            };

            var food = new Food()
            {
                Id = 1,
                Name = "test",
                User = testUser,
                IsAddedToGameNight = false,
                IsAlcoholFree = false,
                IsLactoseFree = false,
                IsNutFree = false,
                IsVegetarian = false,
                GameNight = gameNight
            };

            domainContext.WebsiteUser.Add(testUser);
            domainContext.GameNight.Add(gameNight);
            domainContext.Food.Add(food);
            domainContext.SaveChanges();
            //act
            var result = sut.GetFoodOfUserInGameNight(testUser.Email, gameNight.Id);
            Assert.True(result.Count() == 1);
            domainContext.Database.EnsureDeleted();
        }

        [Fact]
        public void GetFoodOfUserInGameNightReturnsTwoFoodsIfUserHasAddedTwoFoods()
        {
            //arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainContext>().UseInMemoryDatabase("InMemDBWebsiteUser").Options;
            var domainContext = new DomainContext(dbContextOptions);
            var helper = new EFGameNightRepo(domainContext);
            var sut = new EFWebsiteUserRepo(helper, domainContext);

            var testUser = new WebsiteUser()
            {
                Id = 1,
                FirstName = "test",
                LastName = "test",
                Email = "test@test.nl",
                IsVegetarian = true,
                NutAllergy = true,
                LactoseIntolerant = true,
                AlcoholFree = true,
                GameNight = new List<GameNight>()
            };

            var gameNight = new GameNight()
            {
                Organizer = testUser,
                Address = new Address()
                {
                    Street = "test",
                    City = "test",
                    HouseNumber = 1,
                    PostalCode = "test"
                },
                MaxNumberOfPlayers = 1,
                VegetarianSnacks = true,
                LactoseFreeSnacks = true,
                NutFreeSnacks = true,
                AlcoholFreeDrinks = true,
                Participants = new List<WebsiteUser>(),
                Games = new List<BoardGame>(),
                Food = new List<Food>()
            };

            var food = new Food()
            {
                Id = 1,
                Name = "test",
                User = testUser,
                IsAddedToGameNight = false,
                IsAlcoholFree = false,
                IsLactoseFree = false,
                IsNutFree = false,
                IsVegetarian = false,
                GameNight = gameNight
            };

            var food2 = new Food()
            {
                Id = 2,
                Name = "test",
                User = testUser,
                IsAddedToGameNight = false,
                IsAlcoholFree = false,
                IsLactoseFree = false,
                IsNutFree = false,
                IsVegetarian = false,
                GameNight = gameNight
            };

            domainContext.WebsiteUser.Add(testUser);
            domainContext.GameNight.Add(gameNight);
            domainContext.Food.Add(food);
            domainContext.Food.Add(food2);
            domainContext.SaveChanges();
            //act
            var result = sut.GetFoodOfUserInGameNight(testUser.Email, gameNight.Id);
            Assert.True(result.Count() == 2);
            domainContext.Database.EnsureDeleted();
        }

        [Fact]
        public void GetFoodOfUserInGameNightReturnsZeroFoodsIfUserHasAddedZeroFoods()
        {
            //arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainContext>().UseInMemoryDatabase("InMemDBWebsiteUser").Options;
            var domainContext = new DomainContext(dbContextOptions);
            var helper = new EFGameNightRepo(domainContext);
            var sut = new EFWebsiteUserRepo(helper, domainContext);

            var testUser = new WebsiteUser()
            {
                Id = 1,
                FirstName = "test",
                LastName = "test",
                Email = "test@test.nl",
                IsVegetarian = true,
                NutAllergy = true,
                LactoseIntolerant = true,
                AlcoholFree = true,
                GameNight = new List<GameNight>()
            };

            var gameNight = new GameNight()
            {
                Organizer = testUser,
                Address = new Address()
                {
                    Street = "test",
                    City = "test",
                    HouseNumber = 1,
                    PostalCode = "test"
                },
                MaxNumberOfPlayers = 1,
                VegetarianSnacks = true,
                LactoseFreeSnacks = true,
                NutFreeSnacks = true,
                AlcoholFreeDrinks = true,
                Participants = new List<WebsiteUser>(),
                Games = new List<BoardGame>(),
                Food = new List<Food>()
            };

            domainContext.WebsiteUser.Add(testUser);
            domainContext.GameNight.Add(gameNight);
            domainContext.SaveChanges();
            //act
            var result = sut.GetFoodOfUserInGameNight(testUser.Email, gameNight.Id);
            //assert
            Assert.True(result.Count() == 0);
            domainContext.Database.EnsureDeleted();
        }

        [Fact]
        public void AddFoodOfUserToGameNightAddsFoodToGameNight()
        {
            //arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainContext>().UseInMemoryDatabase("InMemDBWebsiteUser").Options;
            var domainContext = new DomainContext(dbContextOptions);
            var helper = new EFGameNightRepo(domainContext);
            var sut = new EFWebsiteUserRepo(helper, domainContext);

            var testUser = new WebsiteUser()
            {
                Id = 1,
                FirstName = "test",
                LastName = "test",
                Email = "test@test.nl",
                IsVegetarian = true,
                NutAllergy = true,
                LactoseIntolerant = true,
                AlcoholFree = true,
                GameNight = new List<GameNight>()
            };

            var gameNight = new GameNight()
            {
                Organizer = testUser,
                Address = new Address()
                {
                    Street = "test",
                    City = "test",
                    HouseNumber = 1,
                    PostalCode = "test"
                },
                MaxNumberOfPlayers = 1,
                VegetarianSnacks = true,
                LactoseFreeSnacks = true,
                NutFreeSnacks = true,
                AlcoholFreeDrinks = true,
                Participants = new List<WebsiteUser>(),
                Games = new List<BoardGame>(),
                Food = new List<Food>()
            };

            var food = new Food()
            {
                Id = 1,
                Name = "test",
                User = testUser,
                IsAddedToGameNight = false,
                IsAlcoholFree = false,
                IsLactoseFree = false,
                IsNutFree = false,
                IsVegetarian = false,
            };


            domainContext.WebsiteUser.Add(testUser);
            domainContext.GameNight.Add(gameNight);
            domainContext.Food.Add(food);
            domainContext.SaveChanges();
            //act
            sut.AddFoodOfUserToGameNight(gameNight.Id, food);
            var result = domainContext.GameNight.Include(f => f.Food).Where(g => g.Id == gameNight.Id).First().Food;
            //assert
            Assert.True(result.Count() == 1);
            domainContext.Database.EnsureDeleted();
        }

        
        [Fact]
        public void DeleteGameNightSignUpDeletesExistingSignUp()
        {
            //arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainContext>().UseInMemoryDatabase("InMemDBWebsiteUser").Options;
            var domainContext = new DomainContext(dbContextOptions);
            var helper = new EFGameNightRepo(domainContext);
            var sut = new EFWebsiteUserRepo(helper, domainContext);

            var testUser = new WebsiteUser()
            {
                Id = 1,
                FirstName = "test",
                LastName = "test",
                Email = "test@test.nl",
                IsVegetarian = true,
                NutAllergy = true,
                LactoseIntolerant = true,
                AlcoholFree = true,
                GameNight = new List<GameNight>()
            };

            var participant = new List<WebsiteUser>();
            participant.Add(testUser);

            var gameNight = new GameNight()
            {
                Organizer = testUser,
                Address = new Address()
                {
                    Street = "test",
                    City = "test",
                    HouseNumber = 1,
                    PostalCode = "test"
                },
                MaxNumberOfPlayers = 1,
                VegetarianSnacks = true,
                LactoseFreeSnacks = true,
                NutFreeSnacks = true,
                AlcoholFreeDrinks = true,
                Participants = participant,
                Games = new List<BoardGame>(),
                Food = new List<Food>()
            };

            var food = new Food()
            {
                Id = 1,
                Name = "test",
                User = testUser,
                IsAddedToGameNight = true,
                IsAlcoholFree = false,
                IsLactoseFree = false,
                IsNutFree = false,
                IsVegetarian = false,
                GameNight = gameNight
            };


            domainContext.WebsiteUser.Add(testUser);
            domainContext.GameNight.Add(gameNight);
            domainContext.Food.Add(food);
            domainContext.SaveChanges();
            //act
            sut.DeleteGameNightSignUp(gameNight.Id, testUser.Email);
            var result = domainContext.GameNight.Include(p => p.Participants).Where(g => g.Id == gameNight.Id).First()
                .Participants.ToList();
            //assert
            Assert.True(result.Count == 0);

            domainContext.Database.EnsureDeleted();

        }

        [Fact]
        public void DeleteGameNightSignUpDeletesFoodOfUserForGameNight()
        {
            //arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainContext>().UseInMemoryDatabase("InMemDBWebsiteUser").Options;
            var domainContext = new DomainContext(dbContextOptions);
            var helper = new EFGameNightRepo(domainContext);
            var sut = new EFWebsiteUserRepo(helper, domainContext);

            var testUser = new WebsiteUser()
            {
                Id = 1,
                FirstName = "test",
                LastName = "test",
                Email = "test@test.nl",
                IsVegetarian = true,
                NutAllergy = true,
                LactoseIntolerant = true,
                AlcoholFree = true,
                GameNight = new List<GameNight>()
            };

            var participant = new List<WebsiteUser>();
            participant.Add(testUser);

            var gameNight = new GameNight()
            {
                Organizer = testUser,
                Address = new Address()
                {
                    Street = "test",
                    City = "test",
                    HouseNumber = 1,
                    PostalCode = "test"
                },
                MaxNumberOfPlayers = 1,
                VegetarianSnacks = true,
                LactoseFreeSnacks = true,
                NutFreeSnacks = true,
                AlcoholFreeDrinks = true,
                Participants = participant,
                Games = new List<BoardGame>(),
                Food = new List<Food>()
            };

            var food = new Food()
            {
                Id = 1,
                Name = "test",
                User = testUser,
                IsAddedToGameNight = true,
                IsAlcoholFree = false,
                IsLactoseFree = false,
                IsNutFree = false,
                IsVegetarian = false,
                GameNight = gameNight
            };


            domainContext.WebsiteUser.Add(testUser);
            domainContext.GameNight.Add(gameNight);
            domainContext.Food.Add(food);
            domainContext.SaveChanges();
            //act
            sut.DeleteGameNightSignUp(gameNight.Id, testUser.Email);
            var result = domainContext.GameNight.Include(p => p.Food).Where(g => g.Id == gameNight.Id).First()
                .Participants.ToList();
            //assert
            Assert.True(result.Count() == 0);
            domainContext.Database.EnsureDeleted();
        }

        [Fact]
        public void GetOrganizedGameNightsReturnsOrganizedGameNights()
        {
            //arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainContext>().UseInMemoryDatabase("InMemDBWebsiteUser").Options;
            var domainContext = new DomainContext(dbContextOptions);
            var helper = new EFGameNightRepo(domainContext);
            var sut = new EFWebsiteUserRepo(helper, domainContext);
            var now = DateTime.Now;
            var date = new DateTime(now.Year + 1, now.Month, now.Day);

            var testUser = new WebsiteUser()
            {
                Id = 1,
                FirstName = "test",
                LastName = "test",
                Email = "test@test.nl",
                IsVegetarian = true,
                NutAllergy = true,
                LactoseIntolerant = true,
                AlcoholFree = true,
                GameNight = new List<GameNight>(),
            };

            var testUser2 = new WebsiteUser()
            {
                Id = 2,
                FirstName = "test",
                LastName = "test",
                Email = "test2@test.nl",
                IsVegetarian = true,
                NutAllergy = true,
                LactoseIntolerant = true,
                AlcoholFree = true,
                GameNight = new List<GameNight>()
            };

            var participant = new List<WebsiteUser>();
            participant.Add(testUser);

            var gameNight = new GameNight()
            {
                Id = 1,
                Organizer = testUser,
                Address = new Address()
                {
                    Street = "test",
                    City = "test",
                    HouseNumber = 1,
                    PostalCode = "test"
                },
                MaxNumberOfPlayers = 1,
                VegetarianSnacks = true,
                LactoseFreeSnacks = true,
                NutFreeSnacks = true,
                AlcoholFreeDrinks = true,
                Participants = new List<WebsiteUser>(),
                Games = new List<BoardGame>(),
                Food = new List<Food>(),
                DateTime = date
            };

            var gameNight2 = new GameNight()
            {
                Id = 2,
                Organizer = testUser2,
                Address = new Address()
                {
                    Street = "test1",
                    City = "test",
                    HouseNumber = 1,
                    PostalCode = "test"
                },
                MaxNumberOfPlayers = 1,
                VegetarianSnacks = true,
                LactoseFreeSnacks = true,
                NutFreeSnacks = true,
                AlcoholFreeDrinks = true,
                Participants = new List<WebsiteUser>(),
                Games = new List<BoardGame>(),
                Food = new List<Food>(),
                DateTime = date
            };

            var gameNight3 = new GameNight()
            {
                Id = 3,
                Organizer = testUser,
                Address = new Address()
                {
                    Street = "test2",
                    City = "test",
                    HouseNumber = 1,
                    PostalCode = "test"
                },
                MaxNumberOfPlayers = 1,
                VegetarianSnacks = true,
                LactoseFreeSnacks = true,
                NutFreeSnacks = true,
                AlcoholFreeDrinks = true,
                Participants = new List<WebsiteUser>(),
                Games = new List<BoardGame>(),
                Food = new List<Food>(),
                DateTime = date
            };



            domainContext.WebsiteUser.Add(testUser);
            domainContext.GameNight.Add(gameNight);
            domainContext.GameNight.Add(gameNight2);
            domainContext.GameNight.Add(gameNight3);
            domainContext.SaveChanges();
            //act
            var result = sut.GetOrganizedGameNights(testUser.Email).ToList();
            //assert
            Assert.True(result.Count() == 2);
            Assert.True(result[0].Id == 1);
            Assert.True(result[1].Id == 3);
            domainContext.Database.EnsureDeleted();
        }

        [Fact]
        public void GetSignedUpGameNightsOnlyReturnsSignedUpGameNights()
        {
            //arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainContext>().UseInMemoryDatabase("InMemDBWebsiteUser").Options;
            var domainContext = new DomainContext(dbContextOptions);
            var helper = new EFGameNightRepo(domainContext);
            var sut = new EFWebsiteUserRepo(helper, domainContext);
            var now = DateTime.Now;
            var date = new DateTime(now.Year + 1, now.Month, now.Day);

            var testUser = new WebsiteUser()
            {
                Id = 1,
                FirstName = "test",
                LastName = "test",
                Email = "test@test.nl",
                IsVegetarian = true,
                NutAllergy = true,
                LactoseIntolerant = true,
                AlcoholFree = true,
                GameNight = new List<GameNight>(),
            };

            var testUser2 = new WebsiteUser()
            {
                Id = 2,
                FirstName = "test",
                LastName = "test",
                Email = "test2@test.nl",
                IsVegetarian = true,
                NutAllergy = true,
                LactoseIntolerant = true,
                AlcoholFree = true,
                GameNight = new List<GameNight>()
            };

            var participant = new List<WebsiteUser>();
            participant.Add(testUser);

            var gameNight = new GameNight()
            {
                Id = 1,
                Organizer = testUser2,
                Address = new Address()
                {
                    Street = "test",
                    City = "test",
                    HouseNumber = 1,
                    PostalCode = "test"
                },
                MaxNumberOfPlayers = 1,
                VegetarianSnacks = true,
                LactoseFreeSnacks = true,
                NutFreeSnacks = true,
                AlcoholFreeDrinks = true,
                Participants = participant,
                Games = new List<BoardGame>(),
                Food = new List<Food>(),
                DateTime = date
            };

            var gameNight2 = new GameNight()
            {
                Id = 2,
                Organizer = testUser2,
                Address = new Address()
                {
                    Street = "test1",
                    City = "test",
                    HouseNumber = 1,
                    PostalCode = "test"
                },
                MaxNumberOfPlayers = 1,
                VegetarianSnacks = true,
                LactoseFreeSnacks = true,
                NutFreeSnacks = true,
                AlcoholFreeDrinks = true,
                Participants = new List<WebsiteUser>(),
                Games = new List<BoardGame>(),
                Food = new List<Food>(),
                DateTime = date
            };

            var gameNight3 = new GameNight()
            {
                Id = 3,
                Organizer = testUser2,
                Address = new Address()
                {
                    Street = "test2",
                    City = "test",
                    HouseNumber = 1,
                    PostalCode = "test"
                },
                MaxNumberOfPlayers = 1,
                VegetarianSnacks = true,
                LactoseFreeSnacks = true,
                NutFreeSnacks = true,
                AlcoholFreeDrinks = true,
                Participants = participant,
                Games = new List<BoardGame>(),
                Food = new List<Food>(),
                DateTime = date
            };



            domainContext.WebsiteUser.Add(testUser);
            domainContext.GameNight.Add(gameNight);
            domainContext.GameNight.Add(gameNight2);
            domainContext.GameNight.Add(gameNight3);
            domainContext.SaveChanges();
            //act
            var result = sut.GetSignedUpGameNights(testUser.Email).ToList();
            //assert
            Assert.True(result.Count() == 2);
            Assert.True(result[0].Id == 1);
            Assert.True(result[1].Id == 3);
            domainContext.Database.EnsureDeleted();
        }


    }
}
