using LightInject;
using SocialGames.TechnicalTest.Games.Interface;

namespace SocialGames.TechnicalTest.Api
{
    public class GamesBootstrapper
    {
        IServiceContainer _serviceContainer;

        public GamesBootstrapper(IServiceContainer container)
        {
            _serviceContainer = container;
        }

        public void Run()
        {
            RegisterServices();
        }

        protected void RegisterServices()
        {
            _serviceContainer.RegisterScoped<IGameService, GameService>();
        }
    }
}
