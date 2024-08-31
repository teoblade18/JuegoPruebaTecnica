using JuegoPruebaTecnica.Models;
using JuegoPruebaTecnica.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;

namespace JuegoPruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartidaService : ControllerBase
    {
        private readonly PartidaController _partidaController;
        private readonly JugadorController _jugadorController;
        private readonly MovimientoController _movimientoController;

        public PartidaService(LocaldbContext context)
        {
            _partidaController = new PartidaController(context);
            _jugadorController = new JugadorController(context);
            _movimientoController = new MovimientoController(context);
        }

        [HttpPost]
        [Route("IniciarPartida")]
        public async Task<ActionResult<Partida>> IniciarPartida([FromBody] List<Jugador> jugadores)
        {
            var partida = await _partidaController.IniciarPartida();

            if (partida != null)
            {
                await _jugadorController.RegistrarJugador(jugadores[0]);
                await _jugadorController.RegistrarJugador(jugadores[1]);
            }
            else
            {
                return BadRequest("Algo salió mal con la creación de la partida");
            }

            return partida;
        }

        [HttpPost]
        [Route("RegistrarMovimiento")]
        public async Task<ActionResult<string>> RegistrarMovimiento([FromBody] Movimiento movimiento)
        {
            var resultado = await _partidaController.RegistrarMovimiento(movimiento);
            return Ok(resultado);
        }

        [HttpGet("VerificarGanador/{idPartida}")]
        public async Task<ActionResult<string>> VerificarGanador(int idPartida)
        {
            var resultado = await _partidaController.VerificarGanador(idPartida);
            return Ok(resultado);
        }
    }
}
