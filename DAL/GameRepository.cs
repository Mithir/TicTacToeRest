using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToe.BL;
using ServiceStack.Redis;
using Entities;
using Entities.Interfaces;

namespace TicTacToe.DAL
{
    public class GameRepository : IGameRepository
    {
        public void SubscribeToStoreEvent(GameStoredEventHandler handler)
        {
            GameStoredEvent += handler;
        }

        public static string NewGames = "urn:g>new:";
        
        public event GameStoredEventHandler GameStoredEvent;

        static class UserGameIndex
        {
            public static string Games(int userId) { return "urn:user>g:" + userId; }
            //public static string UpVotes(long userId) { return "urn:user>q+:" + userId; }
            //public static string DownVotes(long userId) { return "urn:user>q-:" + userId; }
        }

        public static RedisClient redisClient = new RedisClient();
        
        public bool StoreGame(Game game)
        {
            using (var redisGames = redisClient.As<Game>())
            {
                var newGamesIds = redisClient.GetAllItemsFromSet(NewGames);
                if (newGamesIds.Contains(game.Id.ToString()))
                {
                    redisClient.RemoveItemFromSet(NewGames, game.Id.ToString());
                    //redisClient.RemoveItemFromSet(UserGameIndex.Games(game., game.Id.ToString()); 
                    
                }
                OnGameStoredEvent(game);
                redisGames.Store(game);
            }
            return true;
        }

        public Game GetGame(Guid gameGuid)
        {
            using (var redisGames = redisClient.As<Game>())
            {
                return redisGames.GetById(gameGuid);
            }
        }

        public IList<Game> GetAllGames()
        {
            using (var redisGames = redisClient.As<Game>())
            {
                return redisGames.GetAll();
            }
        }

        public bool StoreNewGame(Game game)
        {
            using (var redisGames = redisClient.As<Game>())
            {
                redisGames.Store(game);
                redisClient.AddItemToSet(NewGames, game.Id.ToString());
            }
            return true;
        }

        public Game GetNewestGame(Int32 userId)
        {
            using (var redisGames = redisClient.As<Game>())
            {
                var newGamesIds = redisClient.GetAllItemsFromSet(NewGames);
                
                if (newGamesIds == null || newGamesIds.Count == 0)
                    return null;

                Game newGame = null;
                bool found = false;
                foreach (String gameId in newGamesIds)
                {
                    newGame = redisGames.GetById(gameId);
                    if (newGame.Creator.Id != userId)
                    {
                        found = true;
                        return newGame;
                    }
                }
                if (!found) { return null; };

                return newGame;
            }    
        }
        
        public IList<Game> GetAllGames(int userId)
        {
            using (var redisGames = redisClient.As<Game>())
            {
                var gameIds = redisClient.GetAllItemsFromSet(UserGameIndex.Games(userId));
                var games = redisGames.GetByIds(gameIds);
                return games;
            }
        }

        public void AddGameToUser(Game game, int userId)
        {
            redisClient.AddItemToSet(UserGameIndex.Games(userId),game.Id.ToString());
        }

        public void DeleteDB()
        {
            redisClient.FlushDb();
        }

        protected void OnGameStoredEvent(Game game)
        {
            GameStoredEventArgs e = new GameStoredEventArgs { Game = game };
            GameStoredEvent(this, e);
        } 
    }
}
