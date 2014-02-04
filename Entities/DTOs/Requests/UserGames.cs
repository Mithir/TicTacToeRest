using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack;

namespace Entities.DTOs.Requests
{
    [Route("/user/{userid}/games", "GET")]
    public class UserGames
    {
        public int UserId { get; set; }
    }
}
