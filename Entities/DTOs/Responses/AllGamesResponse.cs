using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToe.BL;

namespace TicTacToe.DTOs.Responses
{
    public class AllGamesResponse
    {
        public IList<Game> Games { get; set; }

    }
}
