using JuegoPruebaTecnica.Models;
using Microsoft.AspNetCore.Mvc;

namespace JuegoPruebaTecnica.Controllers
{
    public class JugadorController : ControllerBase
    {
        private readonly LocaldbContext _context;

        public JugadorController(LocaldbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<Jugador>> RegistrarJugador([FromBody] Jugador jugador)
        {
            if (jugador == null || string.IsNullOrWhiteSpace(jugador.Nombre))
            {
                return BadRequest("El nombre del jugador es obligatorio.");
            }

            _context.Jugadores.Add(jugador);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetJugador), new { id = jugador.IdJugador }, jugador);
        }

        public async Task<ActionResult<Jugador>> GetJugador(int id)
        {
            var jugador = await _context.Jugadores.FindAsync(id);

            if (jugador == null)
            {
                return NotFound();
            }

            return jugador;
        }
    }
}