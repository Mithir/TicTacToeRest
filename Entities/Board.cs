using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe.BL
{
    public class Board
    {
        public PlayerMark[][] Representation{ get; private set; }
        private static readonly int[,] combinations = new int[3,3] { {1,2,4 }, {8,16,32 }, {64,128,256 } };
        private static readonly int[] wins = new int[] { 7, 56, 448, 73, 146, 292, 273, 84 };
        
        public Board()
        {
            Representation = new PlayerMark[3][] {
                                 new PlayerMark[3] ,
                                 new PlayerMark[3] ,
                                 new PlayerMark[3] ,
                               } ;
            
            InitBoard();
        }

        private void InitBoard()
        {
        }

        public void SetMove(int position, PlayerMark playerMark)
        {
            int left;
            int right;
            if (position < 1 || position > 9)
            {
                throw new ArgumentOutOfRangeException("position", position, "Position must be between 1 and 9");
            }
            left = (position-1) % 3;
            right = (position - 1) / 3;
            if (Representation[left][right] != PlayerMark.None)
            {
                throw new InvalidOperationException(String.Format("position {0} is already occupied", position));
            }
            Representation[left][right] = playerMark;
        }

        private int GetCount(PlayerMark playerMark)
        {
            Int32 count = 0;
            foreach (PlayerMark[] rows in Representation)
            {
                foreach (PlayerMark mark in rows)
                {
                    if (mark == playerMark)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public PlayerMark GetWinner()
        {
            Int32 xCounter = 0;
            Int32 oCounter = 0;

            for (Int32 i = 0; i < 3; i++)
            {
                for (Int32 j = 0; j < 3; j++)
                {
                    if (Representation[i][j] == PlayerMark.Circle)
                        oCounter += combinations[i, j];
                    if (Representation[i][j] == PlayerMark.X)
                        xCounter += combinations[i, j];
                }
            }

            if (wins.Contains(oCounter))
                return PlayerMark.Circle;
            if (wins.Contains(xCounter))
                return PlayerMark.X;

            return PlayerMark.None;
        }
    }
}
