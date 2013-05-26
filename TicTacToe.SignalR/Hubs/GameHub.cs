using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToe.BL;
using Entities;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace TicTacToe.SignalR.Hubs
{
    public class GameHub : Hub
    {
        public static Dictionary<int, string> UserConnection = new Dictionary<int, string>();
        public void Send(int userId)
        {
            //Clients.Client(connectionId).refreshGame(game);

            string connectionId = Context.ConnectionId;
            if (UserConnection.ContainsKey(userId))
            {
                UserConnection[userId] = connectionId;
            }
            else
            {
                UserConnection.Add(userId, connectionId);
            }
        }

        public void Register(int userId)
        {
            string connectionId;
            if (UserConnection.TryGetValue(userId, out connectionId))
            {
                connectionId = Context.ConnectionId;
            }
            else 
            {
                UserConnection.Add(userId, connectionId);
            }
        }

        //TODO: if there's an ID when signalr is loaded, this method could be used to hook without registering
        public override Task OnConnected()
        {
            string connectionId = Context.ConnectionId;
            return base.OnConnected();
        }
    }
}
