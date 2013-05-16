using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe.BL
{
    public class Move
    {
        public Int32 ColPosition { get; set; }
        
        public Int32 RowPosition { get; set; }

        public Int32 PlayerId { get; set; }
    }
}
