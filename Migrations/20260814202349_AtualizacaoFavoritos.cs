using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClimaTempoDesafioAPI.Migrations
{
    /// <inheritdoc />
    public partial class AtualizacaoFavoritos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CidadesFavoritas_UserId_Name",
                table: "CidadesFavoritas");

            migrationBuilder.DropColumn(
                name: "Destaque",
                table: "CidadesFavoritas");

            migrationBuilder.AlterColumn<string>(
                name: "Cor",
                table: "CidadesFavoritas",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "CidadesFavoritas",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "CidadesFavoritas",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Tamanho",
                table: "CidadesFavoritas",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_CidadesFavoritas_UserId_Name_Region_Country",
                table: "CidadesFavoritas",
                columns: new[] { "UserId", "Name", "Region", "Country" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CidadesFavoritas_UserId_Name_Region_Country",
                table: "CidadesFavoritas");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "CidadesFavoritas");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "CidadesFavoritas");

            migrationBuilder.DropColumn(
                name: "Tamanho",
                table: "CidadesFavoritas");

            migrationBuilder.AlterColumn<string>(
                name: "Cor",
                table: "CidadesFavoritas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "Destaque",
                table: "CidadesFavoritas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_CidadesFavoritas_UserId_Name",
                table: "CidadesFavoritas",
                columns: new[] { "UserId", "Name" },
                unique: true);
        }
    }
}
