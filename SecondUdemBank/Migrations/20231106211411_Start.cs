using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecondUdemBank.Migrations
{
    /// <inheritdoc />
    public partial class Start : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CuentasDeAhorros",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    id_propietario = table.Column<int>(type: "INTEGER", nullable: false),
                    saldo = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuentasDeAhorros", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "GruposDeAhorros",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    id_CreadorGrupo = table.Column<int>(type: "INTEGER", nullable: false),
                    SaldoGrupo = table.Column<double>(type: "REAL", nullable: false),
                    NombreGrupo = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GruposDeAhorros", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "udemBanks",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    comision = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_udemBanks", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nombre = table.Column<string>(type: "TEXT", nullable: false),
                    clave = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TransaccionesCuentaAhorros",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    id_cuentaDeAhorro = table.Column<int>(type: "INTEGER", nullable: false),
                    CantidadTransaccion = table.Column<double>(type: "REAL", nullable: false),
                    fecha = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    TipoTransaccion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransaccionesCuentaAhorros", x => x.id);
                    table.ForeignKey(
                        name: "FK_TransaccionesCuentaAhorros_CuentasDeAhorros_id_cuentaDeAhorro",
                        column: x => x.id_cuentaDeAhorro,
                        principalTable: "CuentasDeAhorros",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosXGruposAhorros",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    id_ParticipanteGrupo = table.Column<int>(type: "INTEGER", nullable: false),
                    id_GrupoAhorro = table.Column<int>(type: "INTEGER", nullable: false),
                    PerteneceAlGrupo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosXGruposAhorros", x => x.id);
                    table.ForeignKey(
                        name: "FK_UsuariosXGruposAhorros_GruposDeAhorros_id_GrupoAhorro",
                        column: x => x.id_GrupoAhorro,
                        principalTable: "GruposDeAhorros",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuariosXGruposAhorros_Usuarios_id_ParticipanteGrupo",
                        column: x => x.id_ParticipanteGrupo,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prestamos",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    id_usuarioXGrupoDeAhorro = table.Column<int>(type: "INTEGER", nullable: false),
                    cantidadPrestamo = table.Column<double>(type: "REAL", nullable: false),
                    deudaActual = table.Column<double>(type: "REAL", nullable: false),
                    cantidadCuota = table.Column<double>(type: "REAL", nullable: false),
                    cantidadAPagar = table.Column<double>(type: "REAL", nullable: false),
                    fechaPrestamo = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    fechaPlazo = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    interes = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prestamos", x => x.id);
                    table.ForeignKey(
                        name: "FK_Prestamos_UsuariosXGruposAhorros_id_usuarioXGrupoDeAhorro",
                        column: x => x.id_usuarioXGrupoDeAhorro,
                        principalTable: "UsuariosXGruposAhorros",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransaccionesGruposAhorros",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idUsuarioXGrupo = table.Column<int>(type: "INTEGER", nullable: false),
                    CantidadTransaccion = table.Column<double>(type: "REAL", nullable: false),
                    fecha = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    TipoTransaccion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransaccionesGruposAhorros", x => x.id);
                    table.ForeignKey(
                        name: "FK_TransaccionesGruposAhorros_UsuariosXGruposAhorros_idUsuarioXGrupo",
                        column: x => x.idUsuarioXGrupo,
                        principalTable: "UsuariosXGruposAhorros",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_id_usuarioXGrupoDeAhorro",
                table: "Prestamos",
                column: "id_usuarioXGrupoDeAhorro");

            migrationBuilder.CreateIndex(
                name: "IX_TransaccionesCuentaAhorros_id_cuentaDeAhorro",
                table: "TransaccionesCuentaAhorros",
                column: "id_cuentaDeAhorro");

            migrationBuilder.CreateIndex(
                name: "IX_TransaccionesGruposAhorros_idUsuarioXGrupo",
                table: "TransaccionesGruposAhorros",
                column: "idUsuarioXGrupo");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosXGruposAhorros_id_GrupoAhorro",
                table: "UsuariosXGruposAhorros",
                column: "id_GrupoAhorro");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosXGruposAhorros_id_ParticipanteGrupo",
                table: "UsuariosXGruposAhorros",
                column: "id_ParticipanteGrupo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prestamos");

            migrationBuilder.DropTable(
                name: "TransaccionesCuentaAhorros");

            migrationBuilder.DropTable(
                name: "TransaccionesGruposAhorros");

            migrationBuilder.DropTable(
                name: "udemBanks");

            migrationBuilder.DropTable(
                name: "CuentasDeAhorros");

            migrationBuilder.DropTable(
                name: "UsuariosXGruposAhorros");

            migrationBuilder.DropTable(
                name: "GruposDeAhorros");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
