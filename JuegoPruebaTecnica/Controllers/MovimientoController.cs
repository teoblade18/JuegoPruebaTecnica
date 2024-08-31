using JuegoPruebaTecnica.Models;
using Microsoft.AspNetCore.Mvc;

namespace JuegoPruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientoController : ControllerBase
    {
        private readonly LocaldbContext _context;

        public MovimientoController(LocaldbContext context)
        {
            _context = context;
        }

        // POST: api/Movimiento
        [HttpPost]
        public async Task<ActionResult<Movimiento>> RegistrarMovimiento([FromBody] Movimiento movimiento)
        {
            if (movimiento == null || string.IsNullOrWhiteSpace(movimiento.Descripcion))
            {
                return BadRequest("La descripción del movimiento es obligatoria.");
            }

            var partida = await _context.Partidas.FindAsync(movimiento.IdPartida);
            if (partida == null)
            {
                return NotFound("La partida no existe.");
            }

            var jugador = await _context.Jugadores.FindAsync(movimiento.IdJugador);
            if (jugador == null)
            {
                return NotFound("El jugador no existe.");
            }

            _context.Movimientos.Add(movimiento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMovimiento), new { id = movimiento.IdMovimiento }, movimiento);
        }

        // GET: api/Movimiento/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movimiento>> GetMovimiento(int id)
        {
            var movimiento = await _context.Movimientos.FindAsync(id);

            if (movimiento == null)
            {
                return NotFound();
            }

            return movimiento;
        }
    }
}