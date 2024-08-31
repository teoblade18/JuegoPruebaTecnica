using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JuegoPruebaTecnica.Models;

public partial class Jugador
{
    public int? IdJugador { get; set; }

    public string Nombre { get; set; } = null!;

    [JsonIgnoreAttribute]
    public virtual ICollection<Movimiento>? Movimientos { get; set; } = new List<Movimiento>();

    [JsonIgnoreAttribute]
    public virtual ICollection<Partida>? Partidas { get; set; } = new List<Partida>();
}
