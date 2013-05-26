using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToe.DTOs.Responses;
using TicTacToe.DTOs.Requests;
//using TicTacToe.DAL;
using Entities.DTOs.Requests;
using System.IO;
using Entities;
using Entities.Interfaces;

namespace TicTacToe.BL
{
    public class GameCenter : IGameCenter
    {
        public IGameRepository gameRepository { get; set; }

        public GameCenter(IGameRepository repository)
        {
            gameRepository = repository;
        }

        public GameResponse CreateGame(CreateGame gameRequest)
        {
            Board board = new Board();
            Game game = Game.CreateNewGame(gameRequest.PlayerId, gameRequest.Position);
            
            if (gameRepository.StoreNewGame(game))
            {
                gameRepository.AddGameToUser(game, game.Creator.Id);
                return GameResponseFrom(game);
            }
            else return null;
            
        }

        private GameResponse GameResponseFrom(Game game)
        {
            return new GameResponse { Board = game.Board, CirclePlayer = game.Joiner, GameId = game.Id, XPlayer = game.Creator }; 
        }

        public GameResponse GetGame(ExistingGame request)
        {
            Game game = gameRepository.GetGame(request.Id);
            return GameResponseFrom(game); 
        }

        public AllGamesResponse GetAllGames(AllGames request)
        {
            IList<Game> games = gameRepository.GetAllGames();
            return new AllGamesResponse { Games = games };
        }

        public GameResponse MakeAMove(NewMove request)
        {
            Game game = gameRepository.GetGame(request.GameId);
            game.MakeMove(request.PlayerId, request.Position);
            gameRepository.StoreGame(game);
            
            gameRepository.AddGameToUser(game, game.Creator.Id);
            if (game.Joiner != null && game.Joiner.Id != 0)
            {
                gameRepository.AddGameToUser(game, game.Joiner.Id);
            }

            return GameResponseFrom(game);
        }

        public GameResponse JoinNewGame(JoinNewGame request)
        {
            Game game = gameRepository.GetNewestGame(request.UserId);
            if (game == null) return null;
            game.SetOpponent(request.UserId);
            gameRepository.StoreGame(game);
            gameRepository.AddGameToUser(game, request.UserId);
            return GameResponseFrom(game);
        }

        public AllGamesResponse GetUserGames(UserGames request)
        {
            IList<Game> games = gameRepository.GetAllGames(request.UserId);
            return new AllGamesResponse { Games = games };
        }

        public void DeleteAll()
        {
            gameRepository.DeleteDB();
        }
    }
}
