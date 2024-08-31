using System;
using System.Collections.Generic;

namespace JuegoPruebaTecnica.Models;

public partial class Partida
{
    public int IdPartida { get; set; }

    public int? IdGanador { get; set; }

    public virtual Jugador? IdGanadorNavigation { get; set; }

    public virtual ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();
}
