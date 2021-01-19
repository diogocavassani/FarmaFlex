using Microsoft.EntityFrameworkCore.Migrations;

namespace APIFarmaFlex.Infra.Migrations
{
    public partial class PrecoPromocionais : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "PrecoPromocional",
                table: "Produtos",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrecoPromocional",
                table: "Produtos");
        }
    }
}
