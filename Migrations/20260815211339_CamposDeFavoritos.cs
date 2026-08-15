using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClimaTempoDesafioAPI.Migrations
{
    /// <inheritdoc />
    public partial class CamposDeFavoritos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cor",
                table: "CidadesFavoritas");

            migrationBuilder.DropColumn(
                name: "Tamanho",
                table: "CidadesFavoritas");

            migrationBuilder.AddColumn<bool>(
                name: "isExpanded",
                table: "CidadesFavoritas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isExpanded",
                table: "CidadesFavoritas");

            migrationBuilder.AddColumn<string>(
                name: "Cor",
                table: "CidadesFavoritas",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Tamanho",
                table: "CidadesFavoritas",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
