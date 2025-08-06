using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace simulation.lagerverwaltung2.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLagerModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produkte_Kategorien_KategorieId",
                table: "Produkte");

            migrationBuilder.DropTable(
                name: "Kategorien");

            migrationBuilder.CreateTable(
                name: "Kategorie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorie", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Produkte_Kategorie_KategorieId",
                table: "Produkte",
                column: "KategorieId",
                principalTable: "Kategorie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produkte_Kategorie_KategorieId",
                table: "Produkte");

            migrationBuilder.DropTable(
                name: "Kategorie");

            migrationBuilder.CreateTable(
                name: "Kategorien",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorien", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Produkte_Kategorien_KategorieId",
                table: "Produkte",
                column: "KategorieId",
                principalTable: "Kategorien",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
