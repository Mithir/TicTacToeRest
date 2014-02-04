using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack;

namespace TicTacToe.DTOs.Requests
{
    [Route("/game/{Id}", "GET")]
    public class ExistingGame
    {
        public Guid Id { get; set; }
    }
}
