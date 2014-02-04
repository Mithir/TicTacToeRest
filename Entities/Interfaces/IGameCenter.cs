using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.DTOs.Requests;
using TicTacToe.DTOs.Requests;
using TicTacToe.DTOs.Responses;

namespace Entities.Interfaces
{
    public interface IGameCenter
    {
        GameResponse CreateGame(CreateGame request);

        GameResponse GetGame(ExistingGame request);

        GameResponse JoinNewGame(JoinNewGame request);

        AllGamesResponse GetAllGames(AllGames request);

        GameResponse MakeAMove(NewMove request);

        AllGamesResponse GetUserGames(UserGames request);

        void DeleteAll();
    }
}
