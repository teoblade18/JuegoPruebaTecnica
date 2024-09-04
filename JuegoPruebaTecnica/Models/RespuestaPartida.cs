namespace JuegoPruebaTecnica.Models
{
    public class RespuestaPartida
    {
        public Partida Partida { get; set; }
        public List<Jugador> Jugadores { get; set; } = new List<Jugador>();

        public RespuestaPartida() { }
    }
}
