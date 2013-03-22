using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToe.DTOs.Responses;
using TicTacToe.DTOs.Requests;
using TicTacToe.DAL;

namespace TicTacToe.BL
{
    public class GameCenter
    {
        GameRepository gameRepository = new GameRepository();

        public GameResponse CreateGame(CreateGame gameRequest)
        {
            Board board = new Board();
            Game game = Game.CreateNewGame(gameRequest.Mark, gameRequest.PlayerId, gameRequest.Position);
            
            if (gameRepository.StoreNewGame(game))
            {
                return GameResponseFrom(game);
            }
            else return null;
            
        }

        private GameResponse GameResponseFrom(Game game)
        {
            return new GameResponse { Board = game.Board, CirclePlayer = game.CirclePlayer, GameId = game.Id, XPlayer = game.XPlayer }; 
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
            Game game = gameRepository.GetGame(request.Id);
            game.MakeMove(request.Mark, request.Position);
            gameRepository.StoreGame(game);
            return GameResponseFrom(game);
        }

        public GameResponse JoinNewGame(JoinNewGame request)
        {
            Game game = gameRepository.GetNewestGame();
            game.SetOpponent(request.UserId);
            gameRepository.StoreGame(game);
            return GameResponseFrom(game);
        }
    }
}
