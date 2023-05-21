

using Bordspelavond.Data;
using Bordspelavond.Infrastructure.Repo;
using Domain.Models;
using DomainServices.IRepo;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace UnitTests
{
    public class BoardGameTests
    {
        [Fact]
        public void DeleteBoardGameDeletesBoardGameIfBoardGameExists()
        {

            //arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainContext>().UseInMemoryDatabase("BoardGameTestDB").Options;
            var domainContext = new DomainContext(dbContextOptions);
            var sut = new EFGameRepo(domainContext);
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

            var game = new BoardGame()
            {
                Id = 1,
                Name = "test",
                Description = "test",
                Is18Plus = true,
                User = testUser,
                Genre = GameGenres.Strategie,
                TypeOfGame = "test",
                GameNights = new List<GameNight>(),
                Picture = new byte[1],
            };

            domainContext.BoardGame.Add(game);
            domainContext.SaveChanges();

            game.Is18Plus = false;
            //act
            sut.EditBoardGame(game);
            var result = domainContext.BoardGame.ToList();
            //assert
            Assert.False(result[0].Is18Plus);
            domainContext.Database.EnsureDeleted();

        }

        [Fact]
        public void GetAllBoardGamesReturnsAllBoardGames()
        {
            //arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainContext>().UseInMemoryDatabase("BoardGameTestDB").Options;
            var domainContext = new DomainContext(dbContextOptions);
            var sut = new EFGameRepo(domainContext);
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

            var game = new BoardGame()
            {
                Id = 1,
                Name = "test",
                Description = "test",
                Is18Plus = true,
                User = testUser,
                Genre = GameGenres.Strategie,
                TypeOfGame = "test",
                GameNights = new List<GameNight>(),
                Picture = new byte[1],
            };

            var game2 = new BoardGame()
            {
                Id = 2,
                Name = "test",
                Description = "test",
                Is18Plus = true,
                User = testUser,
                Genre = GameGenres.Strategie,
                TypeOfGame = "test",
                GameNights = new List<GameNight>(),
                Picture = new byte[1],
            };

            domainContext.BoardGame.Add(game);
            domainContext.BoardGame.Add(game2);
            domainContext.SaveChanges();
            //act
            var result = sut.GetAllBoardGames();
            //assert
            Assert.True(result.Count() == 2);
            domainContext.Database.EnsureDeleted();
        }

        [Fact]
        public void GetBoardGameByIdReturnsBoardGameOneIfIdIsOne()
        {
            //arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainContext>().UseInMemoryDatabase("BoardGameTestDB").Options;
            var domainContext = new DomainContext(dbContextOptions);
            var sut = new EFGameRepo(domainContext);
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

            var game = new BoardGame()
            {
                Id = 1,
                Name = "test",
                Description = "test",
                Is18Plus = true,
                User = testUser,
                Genre = GameGenres.Strategie,
                TypeOfGame = "test",
                GameNights = new List<GameNight>(),
                Picture = new byte[1],
            };

            var game2 = new BoardGame()
            {
                Id = 2,
                Name = "test",
                Description = "test",
                Is18Plus = true,
                User = testUser,
                Genre = GameGenres.Strategie,
                TypeOfGame = "test",
                GameNights = new List<GameNight>(),
                Picture = new byte[1],
            };

            domainContext.BoardGame.Add(game);
            domainContext.BoardGame.Add(game2);
            domainContext.SaveChanges();
            //act
            var result = sut.GetBoardGameByID(game.Id);
            //assert
            Assert.True(result.Id == 1);
            domainContext.Database.EnsureDeleted();
        }

        [Fact]
        public void GetBoardGameByIdReturnsBoardGameTwoIfIdIsTwo()
        {
            //arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainContext>().UseInMemoryDatabase("BoardGameTestDB").Options;
            var domainContext = new DomainContext(dbContextOptions);
            var sut = new EFGameRepo(domainContext);
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

            var game = new BoardGame()
            {
                Id = 1,
                Name = "test",
                Description = "test",
                Is18Plus = true,
                User = testUser,
                Genre = GameGenres.Strategie,
                TypeOfGame = "test",
                GameNights = new List<GameNight>(),
                Picture = new byte[1],
            };

            var game2 = new BoardGame()
            {
                Id = 2,
                Name = "test",
                Description = "test",
                Is18Plus = true,
                User = testUser,
                Genre = GameGenres.Strategie,
                TypeOfGame = "test",
                GameNights = new List<GameNight>(),
                Picture = new byte[1],
            };

            domainContext.BoardGame.Add(game);
            domainContext.BoardGame.Add(game2);
            domainContext.SaveChanges();
            //act
            var result = sut.GetBoardGameByID(game2.Id);
            //assert
            Assert.True(result.Id == 2);
            domainContext.Database.EnsureDeleted();
        }

        [Fact]
        public void SaveBoarGameSavesBoarGame()
        {
            //arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainContext>().UseInMemoryDatabase("BoardGameTestDB").Options;
            var domainContext = new DomainContext(dbContextOptions);
            var sut = new EFGameRepo(domainContext);
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

            var game = new BoardGame()
            {
                Id = 1,
                Name = "test",
                Description = "test",
                Is18Plus = true,
                User = testUser,
                Genre = GameGenres.Strategie,
                TypeOfGame = "test",
                GameNights = new List<GameNight>(),
                Picture = new byte[1],
            };

            domainContext.WebsiteUser.Add(testUser);
            domainContext.SaveChanges();
            //act
            sut.SaveBoardGame(game);
            var result = domainContext.BoardGame.ToList();
            //assert
            Assert.True(result.Count() == 1);
            domainContext.Database.EnsureDeleted();
        }

        [Fact]
        public void GetBoardGameByUserEmailReturnsBoardGameTestIfEmailIsTest()
        {
            //arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainContext>().UseInMemoryDatabase("BoardGameTestDB").Options;
            var domainContext = new DomainContext(dbContextOptions);
            var sut = new EFGameRepo(domainContext);
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
                Email = "henk@test.nl",
                IsVegetarian = true,
                NutAllergy = true,
                LactoseIntolerant = true,
                AlcoholFree = true,
                GameNight = new List<GameNight>(),
            };

            var game = new BoardGame()
            {
                Id = 1,
                Name = "test",
                Description = "test",
                Is18Plus = true,
                User = testUser,
                Genre = GameGenres.Strategie,
                TypeOfGame = "test",
                GameNights = new List<GameNight>(),
                Picture = new byte[1],
            };

            var game2 = new BoardGame()
            {
                Id = 2,
                Name = "test",
                Description = "test",
                Is18Plus = true,
                User = testUser2,
                Genre = GameGenres.Strategie,
                TypeOfGame = "test",
                GameNights = new List<GameNight>(),
                Picture = new byte[1],
            };

            var game3 = new BoardGame()
            {
                Id = 3,
                Name = "test",
                Description = "test",
                Is18Plus = true,
                User = testUser,
                Genre = GameGenres.Strategie,
                TypeOfGame = "test",
                GameNights = new List<GameNight>(),
                Picture = new byte[1],
            };


            domainContext.BoardGame.Add(game);
            domainContext.BoardGame.Add(game2);
            domainContext.BoardGame.Add(game3);
            domainContext.WebsiteUser.Add(testUser);
            domainContext.WebsiteUser.Add(testUser2);
            domainContext.SaveChanges();
            //act
            var result = sut.GetBoardGamesOfUserByEmail(testUser.Email).ToList();
            //assert
            Assert.True(result.Count() == 2);
            Assert.Equal(1, result[0].Id);
            Assert.Equal(3, result[1].Id);
            domainContext.Database.EnsureDeleted();
        }

        [Fact]
        public void GetBoardGamesOfUserByEmailReturnsBoardGameHenkIfEmailIsHenk()
        {
            //arrange
            var dbContextOptions = new DbContextOptionsBuilder<DomainContext>().UseInMemoryDatabase("BoardGameTestDB").Options;
            var domainContext = new DomainContext(dbContextOptions);
            var sut = new EFGameRepo(domainContext);
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
                Email = "henk@test.nl",
                IsVegetarian = true,
                NutAllergy = true,
                LactoseIntolerant = true,
                AlcoholFree = true,
                GameNight = new List<GameNight>(),
            };

            var game = new BoardGame()
            {
                Id = 1,
                Name = "test",
                Description = "test",
                Is18Plus = true,
                User = testUser,
                Genre = GameGenres.Strategie,
                TypeOfGame = "test",
                GameNights = new List<GameNight>(),
                Picture = new byte[1],
            };

            var game2 = new BoardGame()
            {
                Id = 2,
                Name = "test",
                Description = "test",
                Is18Plus = true,
                User = testUser2,
                Genre = GameGenres.Strategie,
                TypeOfGame = "test",
                GameNights = new List<GameNight>(),
                Picture = new byte[1],
            };

            var game3 = new BoardGame()
            {
                Id = 3,
                Name = "test",
                Description = "test",
                Is18Plus = true,
                User = testUser,
                Genre = GameGenres.Strategie,
                TypeOfGame = "test",
                GameNights = new List<GameNight>(),
                Picture = new byte[1],
            };


            domainContext.BoardGame.Add(game);
            domainContext.BoardGame.Add(game2);
            domainContext.BoardGame.Add(game3);
            domainContext.WebsiteUser.Add(testUser);
            domainContext.WebsiteUser.Add(testUser2);
            domainContext.SaveChanges();
            //act
            var result = sut.GetBoardGamesOfUserByEmail(testUser2.Email).ToList();
            //assert
            Assert.True(result.Count() == 1);
            Assert.Equal(2, result[0].Id);
            domainContext.Database.EnsureDeleted();
        }
    }
}