using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToe.BL;

namespace Entities
{
    public delegate void GameStoredEventHandler(object sender, GameStoredEventArgs e);

    public class GameStoredEventArgs : EventArgs
    {
        public Game Game { get; set; }
    }
}
