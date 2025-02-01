using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FeedbackApp.Migrations
{
    /// <inheritdoc />
    public partial class StoredFunction1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsersWithProjects",
                columns: table => new
                {
                    Id_Uzytkownika = table.Column<int>(type: "integer", nullable: false),
                    Imie = table.Column<string>(type: "text", nullable: false),
                    Nazwisko = table.Column<string>(type: "text", nullable: false),
                    Id_Projektu = table.Column<int>(type: "integer", nullable: false),
                    Nazwa_Projektu = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersWithProjects");
        }
    }
}
