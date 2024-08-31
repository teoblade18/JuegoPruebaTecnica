using System;
using System.Collections.Generic;

namespace JuegoPruebaTecnica.Models;

public partial class Jugador
{
    public int IdJugador { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();

    public virtual ICollection<Partida> Partidas { get; set; } = new List<Partida>();
}
