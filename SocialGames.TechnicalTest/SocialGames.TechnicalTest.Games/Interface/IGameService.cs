using SocialGames.TechnicalTest.Games.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialGames.TechnicalTest.Games.Interface
{
    public interface IGameService
    {
        Task<bool> IsGameValidAsync(string gameId);
        Task<IList<CharIndex>> PlayGameAsync(string gameId);
    }
}
