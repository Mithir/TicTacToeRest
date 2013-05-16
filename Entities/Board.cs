using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe.BL
{
    public class Board
    {
        //public PlayerMark[][] Representation{ get; private set; }
        public int[][] representation{ get; private set; }

        private static readonly int[,] combinations = new int[3, 3] { { 1, 2, 4 }, { 8, 16, 32 }, { 64, 128, 256 } };
        private static readonly int[] wins = new int[] { 7, 56, 448, 73, 146, 292, 273, 84 };

        public Board()
        {
            representation = new int[3][] {
                                 new int[3] ,
                                 new int[3] ,
                                 new int[3] ,
                               };

            InitBoard();
        }

        private void InitBoard(){ }

        internal Boolean SetMove(int colPos, int rowPos, int playerId)
        {
            if (representation[rowPos][colPos] != 0)
            {
                //throw new InvalidOperationException(String.Format("position {0},{1} is already occupied", rowPos, colPos));
                return false;
            }
            representation[rowPos][colPos] = playerId;
            return true;
        }
    }
}
