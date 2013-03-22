using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe.BL
{
    public class Game
    {
        public Boolean Finished { get; set; }
        public Guid Id { get; set; }
        public Board Board { get; set; }
        public Player CirclePlayer { get; set; }
        public Player XPlayer { get; set; }
        public List<Move> Moves { get; set; }
        
        public void MakeMove(PlayerMark playerMark, Int32 position)
        {
            if (Finished) { throw new InvalidOperationException("Game is finished"); }
            if (Moves[Moves.Count - 1].Mark == playerMark) { throw new InvalidOperationException("It is not  your turn"); }

            Board.SetMove(position, playerMark);

            Moves.Add(new Move { Mark = playerMark, Position = position });
            if (Board.GetWinner() != PlayerMark.None)
            {
                Finished = true;
            }
        }


        public static Game CreateNewGame(PlayerMark playerMark, int playerId, int position)
        {
            Game game = new Game {Id = Guid.NewGuid(),  };

            game.MakeFirstMove(playerMark, position);
            if (playerMark == PlayerMark.Circle)
            {
                game.CirclePlayer = new Player { Id = playerId };
            }
            else
            {
                game.XPlayer = new Player { Id = playerId };
            }
            return game;
        }

        private void MakeFirstMove(PlayerMark playerMark, int position)
        {
            Board = new Board();
            Moves = new List<Move>();
            Board.SetMove(position, playerMark);

            Moves.Add(new Move { Mark = playerMark, Position = position });
        }

        public void SetOpponent(int userId)
        {
            if (XPlayer == null)
            {
                XPlayer = new Player { Id = userId };
            }
            else
            {
                CirclePlayer = new Player { Id = userId };
            }
        }
    }
}
