using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.ServiceHost;

namespace TicTacToe.DTOs.Requests
{
    [Route("/game/join","POST")]
    public class JoinNewGame
    {
        public Int32 UserId { get; set; }
    }
}
