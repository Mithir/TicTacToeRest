using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack;
using TicTacToe.BL;

namespace TicTacToe.DTOs.Requests
{
    [Route("/game/", "POST")]
    public class CreateGame
    {
        //public PlayerMark Mark { get; set; }

        public Int32 PlayerId { get; set; }

        public Int32 Position { get; set; }

    }
}
