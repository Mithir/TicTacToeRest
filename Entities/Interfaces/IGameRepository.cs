using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities.Interfaces
{
    public interface IGameRepository
    {
        bool StoreNewGame(TicTacToe.BL.Game game);

        void AddGameToUser(TicTacToe.BL.Game game, int p);

        IList<TicTacToe.BL.Game> GetAllGames();

        TicTacToe.BL.Game GetGame(Guid guid);

        bool StoreGame(TicTacToe.BL.Game game);

        void DeleteDB();

        TicTacToe.BL.Game GetNewestGame(int p);

        IList<TicTacToe.BL.Game> GetAllGames(int p);
    }
}
