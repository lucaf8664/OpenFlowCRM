using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenFlowCRMModels.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CatalogoModelli",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descrizione = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Lunghezza = table.Column<int>(type: "int", nullable: true),
                    Larghezza = table.Column<int>(type: "int", nullable: true),
                    Altezza = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogoModelli", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clienti",
                columns: table => new
                {
                    IDCliente = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Indirizzo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Note = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clienti", x => x.IDCliente);
                });

            migrationBuilder.CreateTable(
                name: "ComponentiMerce",
                columns: table => new
                {
                    IDComponenteMerce = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoComponente = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaterialeComponente = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Lunghezza = table.Column<int>(type: "int", nullable: true),
                    Larghezza = table.Column<int>(type: "int", nullable: true),
                    Altezza = table.Column<int>(type: "int", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(140)", maxLength: 140, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentiMerce", x => x.IDComponenteMerce);
                });

            migrationBuilder.CreateTable(
                name: "Lotti",
                columns: table => new
                {
                    LottoID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Lunghezza = table.Column<int>(type: "int", nullable: false),
                    Larghezza = table.Column<int>(type: "int", nullable: false),
                    Altezza = table.Column<int>(type: "int", nullable: false),
                    Quantita = table.Column<int>(type: "int", nullable: false),
                    TipoMateriale = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Stato = table.Column<int>(type: "int", nullable: false),
                    DataInizioPrevista = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataFinePrevista = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataInizioEffettiva = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataFineEffettiva = table.Column<DateTime>(type: "datetime", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MacchinaAssegnata = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lotti", x => x.LottoID);
                });

            migrationBuilder.CreateTable(
                name: "Macchine",
                columns: table => new
                {
                    MacchineID = table.Column<long>(type: "bigint", nullable: false),
                    LottoCorrente = table.Column<long>(type: "bigint", nullable: false),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IP = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RisorseUmane = table.Column<long>(type: "bigint", nullable: true),
                    X = table.Column<int>(type: "int", nullable: false),
                    Y = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Macchine", x => x.MacchineID);
                });

            migrationBuilder.CreateTable(
                name: "Utenti",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utenti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ordini",
                columns: table => new
                {
                    OrdiniID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cliente = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordini", x => x.OrdiniID);
                    table.ForeignKey(
                        name: "FK_Ordini_Clienti",
                        column: x => x.Cliente,
                        principalTable: "Clienti",
                        principalColumn: "IDCliente");
                });

            migrationBuilder.CreateTable(
                name: "ComponenteMerceCatalogoModelli",
                columns: table => new
                {
                    ComponenteMerceId = table.Column<long>(type: "bigint", nullable: false),
                    CatalogoModelliId = table.Column<long>(type: "bigint", nullable: false),
                    Quantita = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponenteMerceCatalogoModelli", x => new { x.ComponenteMerceId, x.CatalogoModelliId });
                    table.ForeignKey(
                        name: "FK_ComponenteMerceCatalogoModelli_CatalogoModelli",
                        column: x => x.CatalogoModelliId,
                        principalTable: "CatalogoModelli",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ComponenteMerceCatalogoModelli_ComponentiMerce",
                        column: x => x.ComponenteMerceId,
                        principalTable: "ComponentiMerce",
                        principalColumn: "IDComponenteMerce");
                });

            migrationBuilder.CreateTable(
                name: "Partite",
                columns: table => new
                {
                    PartiteID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descrizione = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PezziAlCarico = table.Column<int>(type: "int", nullable: false),
                    NCarichi = table.Column<int>(type: "int", nullable: false),
                    DataConsegna = table.Column<DateTime>(type: "datetime", nullable: false),
                    Modello = table.Column<long>(type: "bigint", nullable: false),
                    Ordine = table.Column<long>(type: "bigint", nullable: true),
                    Stato = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partite", x => x.PartiteID);
                    table.ForeignKey(
                        name: "FK_Ordini_Partite",
                        column: x => x.Ordine,
                        principalTable: "Ordini",
                        principalColumn: "OrdiniID");
                    table.ForeignKey(
                        name: "FK_Partite_Modelli",
                        column: x => x.Modello,
                        principalTable: "CatalogoModelli",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComponenteMerceCatalogoModelli_CatalogoModelliId",
                table: "ComponenteMerceCatalogoModelli",
                column: "CatalogoModelliId");

            migrationBuilder.CreateIndex(
                name: "IX_Ordini_Cliente",
                table: "Ordini",
                column: "Cliente");

            migrationBuilder.CreateIndex(
                name: "IX_Partite_Modello",
                table: "Partite",
                column: "Modello");

            migrationBuilder.CreateIndex(
                name: "IX_Partite_Ordine",
                table: "Partite",
                column: "Ordine");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComponenteMerceCatalogoModelli");

            migrationBuilder.DropTable(
                name: "Lotti");

            migrationBuilder.DropTable(
                name: "Macchine");

            migrationBuilder.DropTable(
                name: "Partite");

            migrationBuilder.DropTable(
                name: "Utenti");

            migrationBuilder.DropTable(
                name: "ComponentiMerce");

            migrationBuilder.DropTable(
                name: "Ordini");

            migrationBuilder.DropTable(
                name: "CatalogoModelli");

            migrationBuilder.DropTable(
                name: "Clienti");
        }
    }
}
