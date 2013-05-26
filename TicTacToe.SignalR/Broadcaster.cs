using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using TicTacToe.SignalR.Hubs;
using Microsoft.AspNet.SignalR;

namespace TicTacToe.SignalR
{
    public class Broadcaster
    {
        public Broadcaster()
        {
            Handler = new GameStoredEventHandler(HandleStoredGame);
        }
        public GameStoredEventHandler Handler { get; set; }

        public void HandleStoredGame(object state, GameStoredEventArgs args)
        {
            string playerId = GetConnectionId(args);
            string connectionId;
            if (playerId == null) { return; }

            if (GameHub.UserConnection.TryGetValue(int.Parse(playerId), out connectionId))
            {
                var context = GlobalHost.ConnectionManager.GetHubContext<GameHub>();
                context.Clients.Client(connectionId).refreshGame(args.Game);
            }
        }

        private string GetConnectionId(GameStoredEventArgs args)
        {
            int userIdSender = args.Game.Moves.Last().PlayerId;
            if (userIdSender == args.Game.Creator.Id) { return args.Game.Joiner.Id.ToString(); }
            if (userIdSender == args.Game.Joiner.Id) { return args.Game.Creator.Id.ToString(); }

            return null;
        }
    }
}
