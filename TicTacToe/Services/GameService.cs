using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceHost;
using TicTacToe.DTOs.Requests;
using TicTacToe.DTOs.Responses;
using TicTacToe.BL;
using Entities.DTOs.Requests;
using ServiceStack.Common.Web;
using Entities.Interfaces;

namespace TicTacToe.Services
{    
    class GameService : Service
    {
        public IGameCenter gameCenter { get; set; }

        public GameResponse Post(CreateGame request)
        {
            return gameCenter.CreateGame(request);
        }

        public GameResponse Get(ExistingGame request)
        {
            GameResponse response = gameCenter.GetGame(request);
            return response;
        }

        public GameResponse Post(JoinNewGame request)
        {
            GameResponse response = gameCenter.JoinNewGame(request);
            if (response == null) throw HttpError.NotFound(String.Format("No new games found"));
            return response;
        }

        public AllGamesResponse Get(AllGames request)
        {
            AllGamesResponse response = gameCenter.GetAllGames(request);
            return response;
        }

        public AllGamesResponse Get(UserGames request)
        {
            AllGamesResponse response = gameCenter.GetUserGames(request);
            return response;
        }

        public GameResponse Put(NewMove request)
        {
            GameResponse response = gameCenter.MakeAMove(request);
            return response;
        }

        public void Post(DeleteAll request)
        {
            gameCenter.DeleteAll();
        }
    }
}
