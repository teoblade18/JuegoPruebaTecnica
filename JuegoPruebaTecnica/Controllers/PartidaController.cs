using JuegoPruebaTecnica.Models;

namespace JuegoPruebaTecnica.Services
{
    public class PartidaController
    {
        private readonly LocaldbContext _context;

        public PartidaController(LocaldbContext context)
        {
            _context = context;
        }

        public async Task<Partida> IniciarPartida()
        {
            var partida = new Partida();
            _context.Partidas.Add(partida);
            await _context.SaveChangesAsync();

            return partida;
        }

        public async Task<string> RegistrarMovimiento(Movimiento movimiento)
        {
            var partida = await _context.Partidas.FindAsync(movimiento.IdPartida);
            if (partida == null)
            {
                throw new Exception("Partida no encontrada.");
            }

            movimiento.Gano = false;
            movimiento.Estado = "Abierto";
            _context.Movimientos.Add(movimiento);
            await _context.SaveChangesAsync();

            return DeterminarResultado(movimiento.IdPartida);
        }

        private string DeterminarResultado(int idPartida)
        {
            var movimientos = _context.Movimientos.Where(p => p.IdPartida == idPartida && p.Estado == "Abierto").ToList();

            if (movimientos.Count() < 2)
            {
                // Esperar a que ambos jugadores hagan sus movimientos
                return "Esperando el movimiento del otro jugador.";
            }

            var movimientoJugador1 = movimientos.First();
            var movimientoJugador2 = movimientos.Last();

            Jugador jugador1 = _context.Jugadores.Where(i => i.IdJugador == movimientoJugador1.IdJugador).First();
            Jugador jugador2 = _context.Jugadores.Where(i => i.IdJugador == movimientoJugador2.IdJugador).First();

            if (movimientoJugador1 == null || movimientoJugador2 == null)
            {
                return "Error en la partida.";
            }

            var resultado = CompararMovimientos(movimientoJugador1.Descripcion, movimientoJugador2.Descripcion);

            if (resultado == 1)
            {
                movimientoJugador1.Gano = true;
                movimientoJugador1.Estado = "Cerrado";
                movimientoJugador2.Estado = "Cerrado";
                _context.SaveChanges();
                return $"{jugador1.Nombre} gana esta ronda con {movimientoJugador1.Descripcion}.";
            }
            else if (resultado == -1)
            {
                movimientoJugador2.Gano = true;
                movimientoJugador1.Estado = "Cerrado";
                movimientoJugador2.Estado = "Cerrado";
                _context.SaveChanges();
                return $"{jugador2.Nombre} gana esta ronda con {movimientoJugador2.Descripcion}.";
            }
            else
            {
                movimientoJugador1.Estado = "Cerrado";
                movimientoJugador2.Estado = "Cerrado";
                _context.SaveChanges();

                return "Empate en esta ronda.";
            }
        }

        private int CompararMovimientos(string movimiento1, string movimiento2)
        {
            if (movimiento1 == movimiento2)
                return 0;

            if ((movimiento1 == "Piedra" && movimiento2 == "Tijera") ||
                (movimiento1 == "Tijera" && movimiento2 == "Papel") ||
                (movimiento1 == "Papel" && movimiento2 == "Piedra"))
            {
                return 1;
            }

            return -1;
        }

        public async Task<string> VerificarGanador(int idPartida)
        {

            var idGanador = _context.Movimientos.GroupBy(m => m.IdJugador)
                                             .Where(g => g.Count(m => m.Gano) >= 3)
                                             .Select(g => g.Key)
                                             .FirstOrDefault();

            var ganador = _context.Jugadores.Find(idGanador);

            if (ganador != null)
            {
                var partida = _context.Partidas.Find(idPartida);
                partida.IdGanador = ganador.IdJugador;
                await _context.SaveChangesAsync();

                return $"El jugador {ganador.Nombre} ha ganado la partida!";
            }

            return "Aún no hay un ganador. Continúa jugando.";
        }
    }
}