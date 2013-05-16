using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToe.BL;
using ServiceStack.Redis;

namespace TicTacToe.DAL
{
    public class GameRepository
    {
        public static string NewGames = "urn:g>new:";

        public static RedisClient redisClient = new RedisClient();
        public bool StoreGame(Game game)
        {
            using (var redisGames = redisClient.As<Game>())
            {
                var newGamesIds = redisClient.GetAllItemsFromSet(NewGames);
                if (newGamesIds.Contains(game.Id.ToString()))
                    redisClient.RemoveItemFromSet(NewGames, game.Id.ToString()); 
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

        public Game GetNewestGame()
        {
            using (var redisGames = redisClient.As<Game>())
            {
                var newGamesIds = redisClient.GetAllItemsFromSet(NewGames);
                if (newGamesIds == null || newGamesIds.Count == 0)
                    return null;

                String newGameId = newGamesIds.FirstOrDefault();
                return redisGames.GetById(newGameId);
                
            }    
        }
    }
}
