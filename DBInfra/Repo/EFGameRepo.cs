using Domain.Models;
using Bordspelavond.Data;
using DomainServices.IRepo;
using System.Security.Claims;

namespace Bordspelavond.Infrastructure.Repo
{

    public class EFGameRepo : IBoardGameRepo
    {
        private readonly DomainContext context;

        public EFGameRepo(DomainContext context)
        {
            this.context = context;
        }

        public void DeleteBoardGame(BoardGame boardGame)
        {
            context.BoardGame.Remove(boardGame);
            context.SaveChanges();
        }

        public void EditBoardGame(BoardGame boardGame)
        {
            BoardGame? gameToChange = context.BoardGame.Where(b => b.Id == boardGame.Id).FirstOrDefault();

            if (gameToChange != null)
            {
                gameToChange.GameNights = boardGame.GameNights;
                gameToChange.Picture = boardGame.Picture;
                gameToChange.Name = boardGame.Name;
                gameToChange.Description = boardGame.Description;
                gameToChange.Genre = boardGame.Genre;
                gameToChange.Is18Plus = boardGame.Is18Plus;
                gameToChange.TypeOfGame = boardGame.TypeOfGame;
                context.SaveChanges();

            }

        }

        public IEnumerable<BoardGame> GetAllBoardGames()
        {
            return context.BoardGame;
        }

        public BoardGame GetBoardGameByID(int id)
        {
            BoardGame? game = context.BoardGame.Find(id);
            if (game != null)
            {
                return game;
            }
            else
            {
                throw new IndexOutOfRangeException("Game with this ID was not found");
            }
        }

        public void SaveBoardGame(BoardGame boardGame)
        {
            var user = context.WebsiteUser.Where(w => w.Email.Equals(boardGame.User.Email)).First();
            boardGame.User = user;
            context.Add(boardGame);
            context.SaveChanges();
        }

        public IEnumerable<BoardGame> GetBoardGamesOfUserByEmail(string Email)
        {
            int? Id = context.WebsiteUser.Where(u => u.Email.Equals(Email)).First().Id;
            if (Id != null)
            {
                var boardGames = context.BoardGame.Where(p => p.User.Id == Id);
                return boardGames;
            }

            throw new Exception("User was not found");
        }

       
    }
}
