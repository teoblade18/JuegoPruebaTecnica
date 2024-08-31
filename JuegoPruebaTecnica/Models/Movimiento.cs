using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JuegoPruebaTecnica.Models;

public partial class Movimiento
{
    public int? IdMovimiento { get; set; }

    public string Descripcion { get; set; } = null!;

    public bool Gano { get; set; }

    public int IdJugador { get; set; }

    public int IdPartida { get; set; }

    [JsonIgnoreAttribute]
    public virtual Jugador? IdJugadorNavigation { get; set; } = null!;

    [JsonIgnoreAttribute]
    public virtual Partida? IdPartidaNavigation { get; set; } = null!;

    public string? Estado { get; set; }
}
