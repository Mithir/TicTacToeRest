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
        
        public void MakeMove(Int32 playerId, Int32 position)
        {
            if (Finished) { throw new InvalidOperationException("Game is finished"); }
            if (Moves != null && Moves.Count>=1 && Moves[Moves.Count - 1].PlayerId == playerId) { throw new InvalidOperationException("It is not your turn"); }

            int rowPos;
            int colPos;
            if (position < 1 || position > 9)
            {
                throw new ArgumentOutOfRangeException("position", position, "Position must be between 1 and 9");
            }
            rowPos = (position - 1) / 3;
            colPos = (position - 1) % 3;

            if (!Board.SetMove(colPos, rowPos, playerId))
            {
                //couldnt save move
            }
            else
            {
                Moves.Add(new Move { PlayerId = playerId, ColPosition = colPos , RowPosition = rowPos});
            }
            //if (Board.GetWinner() != PlayerMark.None)
            //{
            //    Finished = true;
            //}
        }


        public static Game CreateNewGame(int playerId, int position)
        {
            Game game = new Game
            {
                Id = Guid.NewGuid(),
                Board = new Board(),
                XPlayer = new Player { Id = playerId },
                Moves = new List<Move>()
            };

            game.MakeMove(playerId, position);
            //if (playerMark == PlayerMark.Circle)
            //{
            //    game.CirclePlayer = new Player { Id = playerId };
            //}
            //else
            //{
            //    game.XPlayer = new Player { Id = playerId };
            //}
            return game;
        }

        //private void MakeFirstMove(PlayerMark playerMark, int position)
        //{
        //    Board = new Board();
        //    Moves = new List<Move>();
        //    Board.SetMove(position, playerMark);

        //    Moves.Add(new Move { PlayerId = playerMark, Position = position });
        //}

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

        
        public void SetMove(int position, Int32 playerId)
        {
            int left;
            int right;
            if (position < 1 || position > 9)
            {
                throw new ArgumentOutOfRangeException("position", position, "Position must be between 1 and 9");
            }
            left = (position - 1) / 3;
            right = (position - 1) % 3;

            if (!Board.SetMove(right, left, playerId))
            {
                //error
            }
            //if (Representation[left][right] != PlayerMark.None)
            //{
            //    throw new InvalidOperationException(String.Format("position {0} is already occupied", position));
            //}
            //Representation[left][right] = playerMark;
        }

    //    public PlayerMark GetWinner()
    //    {
    //        Int32 xCounter = 0;
    //        Int32 oCounter = 0;

    //        for (Int32 i = 0; i < 3; i++)
    //        {
    //            for (Int32 j = 0; j < 3; j++)
    //            {
    //                if (Representation[i][j] == PlayerMark.Circle)
    //                    oCounter += combinations[i, j];
    //                if (Representation[i][j] == PlayerMark.X)
    //                    xCounter += combinations[i, j];
    //            }
    //        }

    //        if (wins.Contains(oCounter))
    //            return PlayerMark.Circle;
    //        if (wins.Contains(xCounter))
    //            return PlayerMark.X;

    //        return PlayerMark.None;
    //    }
    }
    
}
