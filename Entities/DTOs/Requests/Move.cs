using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToe.BL;
using ServiceStack.ServiceHost;

namespace TicTacToe.DTOs.Requests
{
    [Route("/game/{Id}", "PUT")]
    public class NewMove
    {
        public Guid Id { get; set; }

        public Int32 Position { get; set; }

        public PlayerMark Mark { get; set; }
    }
}
