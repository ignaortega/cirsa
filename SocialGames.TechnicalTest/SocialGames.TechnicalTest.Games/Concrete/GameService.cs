using Microsoft.Extensions.Logging;
using SocialGames.TechnicalTest.Games.Constants;
using SocialGames.TechnicalTest.Games.Model;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SocialGames.TechnicalTest.Games.Interface
{
    public class GameService : IGameService
    {
        private readonly ILogger _logger;

        public GameService(ILogger<GameService> logger)
        {
            this._logger = logger;
        }

        public async Task<bool> IsGameValidAsync(string gameId)
        {
            bool isValid = false;

            switch (gameId)
            {
                case GameIdentifiers.ElTesoroDeJava:
                    isValid = true;
                    break;
            }

            return isValid;
        }

        public async Task<IList<CharIndex>> PlayGameAsync(string gameId)
        {
            try
            {
                var resultList = new List<CharIndex>();
                var gameIdArray = gameId.ToLower().ToCharArray();
                int index = 0;

                while (gameIdArray[index] != 'o')
                {
                    resultList.Add(new CharIndex
                    {
                        Char = gameIdArray[index],
                        Index = index
                    });

                    index++;
                }

                Thread.Sleep(500);

                return resultList;
            }
            catch (Exception e)
            {
                this._logger.LogError(e, string.Format("Error playing {0}", gameId));
                throw e;
            }
        }
    }
}
