using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bordspelavond.Controllers;
using Bordspelavond.Data;
using Bordspelavond.Infrastructure.Repo;
using Domain.Models;
using DomainServices.Logic;
using Microsoft.EntityFrameworkCore;


namespace UnitTests
{

    
    public class GameNightTests
    {
        [Fact]
        public void CanBeEdittedReturnsFalseIfCountIfAtLeastOneSignUp()
        {
            //arrange
            List <WebsiteUser> participants = new List<WebsiteUser>();
            participants.Add(new WebsiteUser()
            {
                Id = 1
            });
            GameNight testGN = new GameNight()
            {
                Participants = participants
            };
            //act
            var result = GameNightLogic.CanBeEditted(testGN);
            //assert
            Assert.False(result);
        }

        [Fact]
        public void CanBeEdittedReturnsTrueIfNoSignUps()
        {

            //arrange
            GameNight testGN = new GameNight()
            {
                
                Participants = new List<WebsiteUser>()
            };
            //act
            var result = GameNightLogic.CanBeEditted(testGN);
            //assert
            Assert.True(result);
        }

        [Fact]
        public void GameNightIsNot18PlusIfItNoGamesAre18Plus()
        {
            //arrange
            List<BoardGame> testGames = new List<BoardGame>();
            testGames.Add(new BoardGame()
            {
                Is18Plus = false
            });

            testGames.Add(new BoardGame()
            {
                Is18Plus = true
            });

            //act

            var result = GameNightLogic.IsStill18Plus(testGames);
            //assert
            Assert.True(result);
        }

        [Fact]
        public void GameNightIs18PlusIfItContains18PlusGame()
        {
            //arrange
            List<BoardGame> testGames = new List<BoardGame>();
            testGames.Add(new BoardGame()
            {
                Is18Plus = false
            });

            testGames.Add(new BoardGame()
            {
                Is18Plus = false
            });

            //act
            var result = GameNightLogic.IsStill18Plus(testGames);
            //assert
            Assert.False(result);
        }

        [Fact]
        public void IsVegetarianReturnsFalseIfGameNightHasNoVegetarianFoods()
        {

            //Arrange
            var testFoodList = new List<Food>();
            testFoodList.Add(new Food()
            {
                IsVegetarian = false
            });

            testFoodList.Add(new Food()
            {
                IsVegetarian = false
            });

            var gameNight = new GameNight()
            {
                Food = testFoodList
            };



            //Act
            var result = GameNightLogic.GameNightIsVegetarian(gameNight);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void IsVegetarianReturnsTrueIfGameNightHasVegetarianFoods()
        {

            //Arrange
            var testFoodList = new List<Food>();
            testFoodList.Add(new Food()
            {
                IsVegetarian = false
            });

            testFoodList.Add(new Food()
            {
                IsVegetarian = true
            });

            var gameNight = new GameNight()
            {
                Food = testFoodList
            };



            //Act
            var result = GameNightLogic.GameNightIsVegetarian(gameNight);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void IsLactoseFreeReturnsFalseIfGameNightHasNoLactoseFreeFoods()
        {

            //Arrange
            var testFoodList = new List<Food>();
            testFoodList.Add(new Food()
            {
                IsLactoseFree = false
            });

            testFoodList.Add(new Food()
            {
                IsLactoseFree = false
            });

            var gameNight = new GameNight()
            {
                Food = testFoodList
            };



            //Act
            var result = GameNightLogic.GameNightIsLactoseFree(gameNight);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void IsLactoseFreeReturnsTrueIfGameNightHasLactoseFreeFoods()
        {

            //Arrange
            var testFoodList = new List<Food>();
            testFoodList.Add(new Food()
            {
                IsLactoseFree = false
            });

            testFoodList.Add(new Food()
            {
                IsLactoseFree = true
            });

            var gameNight = new GameNight()
            {
                Food = testFoodList
            };



            //Act
            var result = GameNightLogic.GameNightIsLactoseFree(gameNight);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void IsNutFreeReturnsFalseIfGameNightHasNoNutFreeFoods()
        {

            //Arrange
            var testFoodList = new List<Food>();
            testFoodList.Add(new Food()
            {
                IsNutFree = false
            });

            testFoodList.Add(new Food()
            {
                IsNutFree = false
            });

            var gameNight = new GameNight()
            {
                Food = testFoodList
            };



            //Act
            var result = GameNightLogic.GameNightIsNutFree(gameNight);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void IsNutFreeReturnsTrueIfGameNightHasNutFreeFoods()
        {

            //Arrange
            var testFoodList = new List<Food>();
            testFoodList.Add(new Food()
            {
                IsNutFree = false
            });

            testFoodList.Add(new Food()
            {
                IsNutFree = true
            });

            var gameNight = new GameNight()
            {
                Food = testFoodList
            };



            //Act
            var result = GameNightLogic.GameNightIsNutFree(gameNight);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void IsAlcoholFreeReturnsFalseIfGameNightHasNoAlcoholFreeFoods()
        {

            //Arrange
            var testFoodList = new List<Food>();
            testFoodList.Add(new Food()
            {
                IsAlcoholFree = false
            });

            testFoodList.Add(new Food()
            {
                IsAlcoholFree = false
            });

            var gameNight = new GameNight()
            {
                Food = testFoodList
            };



            //Act
            var result = GameNightLogic.GameNightIsAlcoholFree(gameNight);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void IsAlcoholFreeReturnsTrueIfGameNightHasAlcoholFreeFoods()
        {

            //Arrange
            var testFoodList = new List<Food>();
            testFoodList.Add(new Food()
            {
                IsAlcoholFree = false
            });

            testFoodList.Add(new Food()
            {
                IsAlcoholFree= true
            });

            var gameNight = new GameNight()
            {
                Food = testFoodList
            };



            //Act
            var result = GameNightLogic.GameNightIsAlcoholFree(gameNight);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void NewGameNightIsAdded()
        {
            var dbContextOptions = new DbContextOptionsBuilder<DomainContext>().UseInMemoryDatabase("InMemDB").Options;
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
            domainContext.WebsiteUser.Add(testUser);
            var sut = new EFGameNightRepo(domainContext);
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
            sut.SaveGameNight(gameNight);

            Assert.True(domainContext.GameNight.Count() == 1);
            domainContext.Database.EnsureDeleted();
        }
        [Fact]
        public void DeleteGameNightDeletesGameNight()
        {
            //arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainContext>().UseInMemoryDatabase("InMemDB").Options;
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
            domainContext.WebsiteUser.Add(testUser);
            var sut = new EFGameNightRepo(domainContext);
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
            domainContext.GameNight.Add(gameNight);

            //act
            sut.DeleteGameNight(gameNight);

            Assert.True(domainContext.GameNight.Count() == 0);
            domainContext.Database.EnsureDeleted();
        }


        [Fact]
        public void EditGameNightEditsGameNight()
        {
            //arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainContext>().UseInMemoryDatabase("InMemDB").Options;
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
            domainContext.WebsiteUser.Add(testUser);
            var sut = new EFGameNightRepo(domainContext);
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
                Food = new List<Food>()
            };
            domainContext.GameNight.Add(gameNight);
            domainContext.SaveChanges();
            gameNight.NutFreeSnacks = false;
            //act
            sut.EditGameNight(gameNight);
            var result = domainContext.GameNight.Find(1).NutFreeSnacks;
            //assert
            Assert.False(result);
            domainContext.Database.EnsureDeleted();

        }

        [Fact]
        public void EditGameNightEditsAddressIfAddressDoesNotExist()
        {
            //arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainContext>().UseInMemoryDatabase("InMemDB").Options;
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
            domainContext.WebsiteUser.Add(testUser);
            var sut = new EFGameNightRepo(domainContext);
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
                Food = new List<Food>()
            };
            domainContext.GameNight.Add(gameNight);
            domainContext.SaveChanges();
            gameNight.Address = new Address()
            {
                Street = "test2",
                City = "test2",
                HouseNumber = 2,
                PostalCode = "test2"
            };
            //act
            sut.EditGameNight(gameNight);
            var result = domainContext.GameNight.Find(1).Address;
            //assert
            Assert.Equal("test2", result.City);
            Assert.Equal("test2", result.Street);
            Assert.Equal(2, result.HouseNumber);
            Assert.Equal("test2", result.PostalCode);
            domainContext.Database.EnsureDeleted();

        }

        [Fact]
        public void EditGameNightEditsIsVegetarianToTrueIfVegetarianFoodIsAdded()
        {
            //arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainContext>().UseInMemoryDatabase("InMemDB").Options;
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
            domainContext.WebsiteUser.Add(testUser);
            var sut = new EFGameNightRepo(domainContext);
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
                Food = new List<Food>()
            };
            domainContext.GameNight.Add(gameNight);
            domainContext.SaveChanges();
            var foodList = new List<Food>();
            foodList.Add(new Food()
            {
                Name = "Test",
                IsVegetarian = true,
                IsAlcoholFree = false,
                IsNutFree = false,
                IsLactoseFree = false,
                IsAddedToGameNight = true,
            });

            gameNight.Food = foodList;
            //act
            sut.EditGameNight(gameNight);
            var result = domainContext.GameNight.Find(1).VegetarianSnacks;
            //assert
            Assert.True(result);
            domainContext.Database.EnsureDeleted();

        }

        [Fact]
        public void EditGameNightEditsIsVegetarianToFalseIfVegetarianFoodIsRemoved()
        {
            //arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainContext>().UseInMemoryDatabase("InMemDB").Options;
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
            domainContext.WebsiteUser.Add(testUser);
            var sut = new EFGameNightRepo(domainContext);
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
                Food = new List<Food>()
            };
            var foodList = new List<Food>();
            foodList.Add(new Food()
            {
                Name = "Test",
                IsVegetarian = true,
                IsAlcoholFree = false,
                IsNutFree = false,
                IsLactoseFree = false,
                IsAddedToGameNight = true,
            });

            gameNight.Food = foodList;

            foodList[0].IsVegetarian = false;
            gameNight.Food = foodList;
            domainContext.GameNight.Add(gameNight);
            domainContext.SaveChanges();

            //act
            sut.EditGameNight(gameNight);
            var result = domainContext.GameNight.Find(1).VegetarianSnacks;
            //assert
            Assert.False(result);
            domainContext.Database.EnsureDeleted();
        }

        [Fact]
        public void GetMostRecentGameNightOfUserByEmailReturnsMostRecentGameNight()
        {
            //arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainContext>().UseInMemoryDatabase("InMemDB").Options;
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

            var testUser2 = new WebsiteUser()
            {
                Id = 2,
                FirstName = "test2",
                LastName = "test2",
                Email = "test2@test.nl",
                IsVegetarian = true,
                NutAllergy = true,
                LactoseIntolerant = true,
                AlcoholFree = true,
                GameNight = new List<GameNight>()
            };
            domainContext.WebsiteUser.Add(testUser);
            var sut = new EFGameNightRepo(domainContext);
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
                Food = new List<Food>()
            };

            var gameNight2 = new GameNight()
            {
                Id = 2,
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
                Food = new List<Food>()
            };

            var gameNight3 = new GameNight()
            {
                Id = 3,
                Organizer = testUser2,
                Address = new Address()
                {
                    Street = "test3",
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

            domainContext.GameNight.Add(gameNight);
            domainContext.GameNight.Add(gameNight2);
            domainContext.GameNight.Add(gameNight3);
            domainContext.SaveChanges();

            //act
            var result = sut.GetMostRecentGameNightOfUserByEmail("test@test.nl").Id;
            //assert
            Assert.Equal(2, result);
            domainContext.Database.EnsureDeleted();
        }

        [Fact]
        public void GetBoardGameNotInGameNightReturnsBoardGameNotInGameNight()
        {
            //arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainContext>().UseInMemoryDatabase("InMemDB").Options;
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
            domainContext.WebsiteUser.Add(testUser);
            var sut = new EFGameNightRepo(domainContext);
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
                Games = new List<BoardGame>(),
                Participants = new List<WebsiteUser>(),
                Food = new List<Food>()
            };

            var game1 = new BoardGame()
            {
                Id = 1,
                Genre = GameGenres.Behendigheid,
                Description = "test",
                Is18Plus = true,
                Picture = new byte[1],
                Name = "test",
                TypeOfGame = "test",
                User = testUser,

            };

            var game2 = new BoardGame()
            {
                Id = 2,
                Genre = GameGenres.Behendigheid,
                Description = "test",
                Is18Plus = true,
                Picture = new byte[1],
                Name = "test",
                TypeOfGame = "test",
                User = testUser,

            };


            gameNight.Games.Add(game1);
            domainContext.GameNight.Add(gameNight);
            domainContext.BoardGame.Add(game2);
            domainContext.SaveChanges();

            //act
            sut.EditGameNight(gameNight);
            var result = sut.GetBoardGamesNotInGameNight(gameNight).ToList();
            //assert
            Assert.True(result.Count == 1);
            Assert.True(result[0].Id == 2);
            domainContext.Database.EnsureDeleted();
        }

        [Fact]
        public void DeleteGameFromGameNightDeletesNoGameFromGameNightIfGameIsNotInGameNight()
        {
            //arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainContext>().UseInMemoryDatabase("InMemDB").Options;
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
            domainContext.WebsiteUser.Add(testUser);
            var sut = new EFGameNightRepo(domainContext);
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
                Games = new List<BoardGame>(),
                Participants = new List<WebsiteUser>(),
                Food = new List<Food>()
            };

            var game1 = new BoardGame()
            {
                Id = 1,
                Genre = GameGenres.Behendigheid,
                Description = "test",
                Is18Plus = true,
                Picture = new byte[1],
                Name = "test",
                TypeOfGame = "test",
                User = testUser,

            };

            var game2 = new BoardGame()
            {
                Id = 2,
                Genre = GameGenres.Behendigheid,
                Description = "test",
                Is18Plus = true,
                Picture = new byte[1],
                Name = "test",
                TypeOfGame = "test",
                User = testUser,

            };
            var game3 = new BoardGame()
            {
                Id = 3,
                Genre = GameGenres.Behendigheid,
                Description = "test",
                Is18Plus = true,
                Picture = new byte[1],
                Name = "test",
                TypeOfGame = "test",
                User = testUser,

            };


            gameNight.Games.Add(game1);
            gameNight.Games.Add(game2);
            gameNight.Games.Add(game3);
            domainContext.GameNight.Add(gameNight);
            domainContext.SaveChanges();
            //act
            sut.EditGameNight(gameNight);
            sut.DeleteGameFromGameNight(3, 1);
            var result = domainContext.GameNight.Include(gn => gn.Games).Where(gn => gn.Id == 1).First();
            //assert
            Assert.True(result.Games.Count == 2);
            domainContext.Database.EnsureDeleted();
        }

        [Fact]
        public void DeleteGameFromGameNightDeletesGameFromGameNightIfGameIsInGameNight()
        {
            //arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainContext>().UseInMemoryDatabase("InMemDB").Options;
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
            domainContext.WebsiteUser.Add(testUser);
            var sut = new EFGameNightRepo(domainContext);
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
                Games = new List<BoardGame>(),
                Participants = new List<WebsiteUser>(),
                Food = new List<Food>()
            };

            var game1 = new BoardGame()
            {
                Id = 1,
                Genre = GameGenres.Behendigheid,
                Description = "test",
                Is18Plus = true,
                Picture = new byte[1],
                Name = "test",
                TypeOfGame = "test",
                User = testUser,

            };

            var game2 = new BoardGame()
            {
                Id = 2,
                Genre = GameGenres.Behendigheid,
                Description = "test",
                Is18Plus = true,
                Picture = new byte[1],
                Name = "test",
                TypeOfGame = "test",
                User = testUser,

            };


            gameNight.Games.Add(game1);
            gameNight.Games.Add(game2);
            domainContext.GameNight.Add(gameNight);
            domainContext.SaveChanges();
            //act
            sut.EditGameNight(gameNight);
            sut.DeleteGameFromGameNight(1, 1);
            var result = domainContext.GameNight.Include(gn => gn.Games).Where(gn => gn.Id == 1).First();
            //assert
            Assert.True(result.Games.Count == 1);
            Assert.True(result.Games.ToList()[0].Id == 2);
            domainContext.Database.EnsureDeleted();
        }

        [Fact]
        public void AddGameToGameNightAddsGameToGameNight()
        {
            //arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainContext>().UseInMemoryDatabase("InMemDB").Options;
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
            domainContext.WebsiteUser.Add(testUser);
            var sut = new EFGameNightRepo(domainContext);
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
                Games = new List<BoardGame>(),
                Participants = new List<WebsiteUser>(),
                Food = new List<Food>()
            };

            var game1 = new BoardGame()
            {
                Id = 1,
                Genre = GameGenres.Behendigheid,
                Description = "test",
                Is18Plus = true,
                Picture = new byte[1],
                Name = "test",
                TypeOfGame = "test",
                User = testUser,

            };
            domainContext.BoardGame.Add(game1);
            domainContext.GameNight.Add(gameNight);
            domainContext.SaveChanges();
            //act
            sut.AddGameToGameNight(1, 1);
            var result = domainContext.GameNight.Include(gn => gn.Games).Where(gn => gn.Id == 1).First();
            //assert
            Assert.True(result.Games.Count == 1);
            domainContext.Database.EnsureDeleted();
        }
    }
}
