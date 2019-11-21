using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Barabinot.MiniBicks.Data.Migrations
{
    public partial class AddFrais : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Frais",
                columns: table => new
                {
                    FraisId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateFrais = table.Column<DateTime>(nullable: false),
                    TotFrais = table.Column<decimal>(nullable: false),
                    ReportFrais = table.Column<decimal>(nullable: false),
                    UtilisateursUserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frais", x => x.FraisId);
                    table.ForeignKey(
                        name: "FK_Frais_Utilisateurs_UtilisateursUserId",
                        column: x => x.UtilisateursUserId,
                        principalTable: "Utilisateurs",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Frais_UtilisateursUserId",
                table: "Frais",
                column: "UtilisateursUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Frais");
        }
    }
}
