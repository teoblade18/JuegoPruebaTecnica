using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JuegoPruebaTecnica.Models;

public partial class Partida
{
    public int IdPartida { get; set; }

    public int? IdGanador { get; set; }

    public virtual Jugador? IdGanadorNavigation { get; set; }

    [JsonIgnoreAttribute]
    public virtual ICollection<Movimiento>? Movimientos { get; set; } = new List<Movimiento>();
}
