using Microsoft.EntityFrameworkCore.Migrations;

namespace Barabinot.MiniBicks.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conge",
                columns: table => new
                {
                    CongeId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NbConge = table.Column<int>(nullable: false),
                    NbRTT = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conge", x => x.CongeId);
                });

            migrationBuilder.CreateTable(
                name: "Utilisateurs",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nom = table.Column<string>(nullable: true),
                    Prenom = table.Column<string>(nullable: true),
                    Rue = table.Column<string>(nullable: true),
                    CodePostal = table.Column<string>(nullable: true),
                    Ville = table.Column<string>(nullable: true),
                    Pays = table.Column<string>(nullable: true),
                    Role = table.Column<int>(nullable: false),
                    IdSuperieur = table.Column<int>(nullable: false),
                    CongeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateurs", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Utilisateurs_Conge_CongeId",
                        column: x => x.CongeId,
                        principalTable: "Conge",
                        principalColumn: "CongeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateurs_CongeId",
                table: "Utilisateurs",
                column: "CongeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Utilisateurs");

            migrationBuilder.DropTable(
                name: "Conge");
        }
    }
}
