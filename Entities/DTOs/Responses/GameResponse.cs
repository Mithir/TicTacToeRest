using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToe.BL;

namespace TicTacToe.DTOs.Responses
{
    [Serializable]
    public class GameResponse
    {
        public Guid GameId { get; set; }

        public Board Board { get; set; }

        public Player CirclePlayer { get; set; }

        public Player XPlayer { get; set; }
    }
}
