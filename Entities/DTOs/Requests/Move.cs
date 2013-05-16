using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToe.BL;
using ServiceStack.ServiceHost;

namespace TicTacToe.DTOs.Requests
{
    [Route("/game/", "PUT")]
    public class NewMove
    {
        public Guid GameId { get; set; }

        public Int32 Position { get; set; }

        public Int32 PlayerId { get; set; }
    }
}
