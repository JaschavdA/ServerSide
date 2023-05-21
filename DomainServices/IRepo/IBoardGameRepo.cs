using Domain.Models;

namespace DomainServices.IRepo
{
    public interface IBoardGameRepo
    {
        IEnumerable<BoardGame> GetAllBoardGames();
        void SaveBoardGame(BoardGame boardGame);
        void EditBoardGame(BoardGame boardGame);
        void DeleteBoardGame(BoardGame boardGame);
        BoardGame GetBoardGameByID(int id);
        IEnumerable<BoardGame> GetBoardGamesOfUserByEmail(string email);

    }
}