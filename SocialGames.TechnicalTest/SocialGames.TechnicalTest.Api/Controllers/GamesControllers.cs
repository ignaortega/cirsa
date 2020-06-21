using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialGames.TechnicalTest.Games.Interface;
using System;
using System.Threading.Tasks;

namespace SocialGames.TechnicalTest.Api.Controllers
{
    [ApiController]
    public class GamesControllers : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IGameService _gameService;

        public GamesControllers(IGameService gameService, ILogger<GamesControllers> logger)
        {
            this._logger = logger;
            this._gameService = gameService;
        }

        [Route("api/games/{gameId}/play")]
        [HttpPost]
        public async Task<IActionResult> Play(string gameId)
        {
            try
            {
                // Ya que el método de la API es simplemente "Play(gameId)"
                // Asumo que la idea del mismo es que se use de forma genérica 
                // para mas de un juego aunque actualmente solo exista "El Tesoro de Java"
                // por eso delego la comprobación de si el juego es válido en el servicio
                var isGameValid = await this._gameService.IsGameValidAsync(gameId);

                if (isGameValid)
                {
                    // De forma similar, si el método Play es génerico
                    // delego la lógica del juego puntual en el servicio.
                    // El método play podría determinar algun otro método 
                    // puntual del servicio en función del id, por ejemplo:
                    // _gameService.PlayElTesoroDeJava
                    // Sin mas detalles sobre el modelo de juegos, y en pos de 
                    // la simplicidad, lo dejé como otro método genérico y que
                    // eventualemnte sea el servicio el que se encarge de 
                    // elegir que juegue jugar
                    var result = await this._gameService.PlayGameAsync(gameId);

                    return Ok(result);
                }
                else
                {
                    // Si el juego no es válido, devuelve un código de estatus 400
                    // indicando de que el juego no es válido
                    return BadRequest(string.Format("The game {0} is invalid", gameId));
                }
            }
            catch (Exception e)
            {
                // Ante cualquier error, se devuelve al cliente un código de estatus 500
                // indicando de que hubo un problema con la aplicacion
                // y se guarda registro detallado del error
                this._logger.LogError(e, this.Url.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
