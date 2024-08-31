using System;
using System.Collections.Generic;

namespace JuegoPruebaTecnica.Models;

public partial class Movimiento
{
    public int IdMovimiento { get; set; }

    public string Descripcion { get; set; } = null!;

    public bool Gano { get; set; }

    public int IdJugador { get; set; }

    public int IdPartida { get; set; }

    public virtual Jugador IdJugadorNavigation { get; set; } = null!;

    public virtual Partida IdPartidaNavigation { get; set; } = null!;
}
