using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nextflow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IncreaseIbgeCodeLengthInCities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IbgeCode",
                table: "cities", // Certifique-se que o nome da tabela está igual ao do banco (minusculo ou PascalCase)
                type: "character varying(7)",
                maxLength: 7,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(2)",
                oldMaxLength: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // CUIDADO: Isso vai falhar se já existirem cidades cadastradas com código > 2 digitos
            migrationBuilder.AlterColumn<string>(
                name: "IbgeCode",
                table: "cities",
                type: "character varying(2)",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(7)",
                oldMaxLength: 7);
        }
    }
}