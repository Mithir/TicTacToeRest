using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToe.DTOs.Responses;

namespace Entities.Interfaces
{
    public interface IGameCenter
    {
        GameResponse CreateGame(TicTacToe.DTOs.Requests.CreateGame request);

        GameResponse GetGame(TicTacToe.DTOs.Requests.ExistingGame request);

        GameResponse JoinNewGame(TicTacToe.DTOs.Requests.JoinNewGame request);

        AllGamesResponse GetAllGames(TicTacToe.DTOs.Requests.AllGames request);

        GameResponse MakeAMove(TicTacToe.DTOs.Requests.NewMove request);

        AllGamesResponse GetUserGames(Entities.DTOs.Requests.UserGames request);

        void DeleteAll();
    }
}
