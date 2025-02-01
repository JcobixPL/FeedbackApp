using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FeedbackApp.Migrations
{
    /// <inheritdoc />
    public partial class StoredFunction2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectsWithFeedbacks",
                columns: table => new
                {
                    Id_Projektu = table.Column<int>(type: "integer", nullable: false),
                    Nazwa_Projektu = table.Column<string>(type: "text", nullable: false),
                    Id_Feedbacku = table.Column<int>(type: "integer", nullable: false),
                    Typ = table.Column<string>(type: "text", nullable: false),
                    Priorytet = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Opis_Feedbacku = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectsWithFeedbacks");
        }
    }
}
