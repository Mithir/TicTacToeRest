using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToe.BL;

namespace Entities.Interfaces
{
    public interface IGameRepository
    {
        bool StoreNewGame(Game game);

        void AddGameToUser(Game game, int p);

        IList<Game> GetAllGames();

        Game GetGame(Guid guid);

        bool StoreGame(Game game);

        void DeleteDB();

        Game GetNewestGame(int p);

        IList<Game> GetAllGames(int p);
    }
}
