using JuegoPruebaTecnica.Models;
using JuegoPruebaTecnica.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;

namespace JuegoPruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("ReglasCors")]
    public class PartidaService : ControllerBase
    {
        private readonly PartidaController _partidaController;
        private readonly JugadorController _jugadorController;

        public PartidaService(PartidaController partidaController, JugadorController jugadorController)
        {
            _partidaController = partidaController;
            _jugadorController = jugadorController;
        }

        [HttpPost]
        [Route("IniciarPartida")]
        [EnableCors("ReglasCors")]
        public async Task<ActionResult<RespuestaPartida>> IniciarPartida([FromBody] List<Jugador> jugadores)
        {
            try
            {
                RespuestaPartida respuestaPartida = new RespuestaPartida();
                ActionResult<Jugador> jugador1;
                ActionResult<Jugador> jugador2;

                respuestaPartida.Partida = await _partidaController.IniciarPartida();

                if (respuestaPartida.Partida != null)
                {
                    jugador1 = await _jugadorController.RegistrarJugador(jugadores[0]);
                    jugador2 = await _jugadorController.RegistrarJugador(jugadores[1]);
                }
                else
                {
                    return BadRequest("Algo salió mal con la creación de la partida");
                }

                if (jugador1.Result is CreatedAtActionResult createdResult)
                {
                    var jugadorRegistrado = createdResult.Value as Jugador;
                    if (jugadorRegistrado != null)
                    {
                        respuestaPartida.Jugadores.Add(jugadorRegistrado);
                    }
                }
                else if (jugador1.Result is BadRequestObjectResult)
                {
                    return BadRequest("Algo salió mal con la creación del primer jugador");
                }

                if (jugador2.Result is CreatedAtActionResult createdResult2)
                {
                    var jugadorRegistrado = createdResult2.Value as Jugador;
                    if (jugadorRegistrado != null)
                    {
                        respuestaPartida.Jugadores.Add(jugadorRegistrado);
                    }
                }
                else if (jugador2.Result is BadRequestObjectResult)
                {
                    return BadRequest("Algo salió mal con la creación del segundo jugador");
                }

                return respuestaPartida;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("RegistrarMovimiento")]
        [EnableCors("ReglasCors")]
        public async Task<ActionResult<string>> RegistrarMovimiento([FromBody] Movimiento movimiento)
        {
            try
            {
                var resultado = await _partidaController.RegistrarMovimiento(movimiento);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("VerificarGanador/{idPartida}")]
        [EnableCors("ReglasCors")]
        public async Task<ActionResult<string>> VerificarGanador(int idPartida)
        {
            try
            {
                var resultado = await _partidaController.VerificarGanador(idPartida);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
