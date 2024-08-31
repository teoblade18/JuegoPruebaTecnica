using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JuegoPruebaTecnica.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jugador",
                columns: table => new
                {
                    IdJugador = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Jugador__99E32016D4D44748", x => x.IdJugador);
                });

            migrationBuilder.CreateTable(
                name: "Partidas",
                columns: table => new
                {
                    IdPartida = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdGanador = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Partida__6ED660C70B4423B7", x => x.IdPartida);
                    table.ForeignKey(
                        name: "FK__Partida__IdGanad__4BAC3F29",
                        column: x => x.IdGanador,
                        principalTable: "Jugador",
                        principalColumn: "IdJugador");
                });

            migrationBuilder.CreateTable(
                name: "Movimiento",
                columns: table => new
                {
                    IdMovimiento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Gano = table.Column<bool>(type: "bit", nullable: false),
                    IdJugador = table.Column<int>(type: "int", nullable: false),
                    IdPartida = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Movimien__881A6AE081A980D3", x => x.IdMovimiento);
                    table.ForeignKey(
                        name: "FK__Movimient__IdJug__4E88ABD4",
                        column: x => x.IdJugador,
                        principalTable: "Jugador",
                        principalColumn: "IdJugador");
                    table.ForeignKey(
                        name: "FK__Movimient__IdPar__4F7CD00D",
                        column: x => x.IdPartida,
                        principalTable: "Partidas",
                        principalColumn: "IdPartida");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movimiento_IdJugador",
                table: "Movimiento",
                column: "IdJugador");

            migrationBuilder.CreateIndex(
                name: "IX_Movimiento_IdPartida",
                table: "Movimiento",
                column: "IdPartida");

            migrationBuilder.CreateIndex(
                name: "IX_Partidas_IdGanador",
                table: "Partidas",
                column: "IdGanador");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movimiento");

            migrationBuilder.DropTable(
                name: "Partidas");

            migrationBuilder.DropTable(
                name: "Jugador");
        }
    }
}
